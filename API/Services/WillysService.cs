using System.Text.Json;
using API.Mappers;
using API.Requests;
using Database;

namespace API.Properties.Services;

public interface IWillysService
{
    Task<bool> GetDiscountedProducts(GetDiscountedItemsWillysRequest req);
}
public class WillysService : IWillysService
{
    private readonly HttpClient _httpClient;
    private WebApiDbContext _webApiDbContext;
    public WillysService(HttpClient httpClient, WebApiDbContext webApiDbContext)
    {
        _httpClient = httpClient;
        _webApiDbContext = webApiDbContext;
    }
    public async Task<bool> GetDiscountedProducts(GetDiscountedItemsWillysRequest req)
    {
        var store = _webApiDbContext.Stores.FirstOrDefault(x => x.InternalStoreId == req.StoreId);
        
        var productList = new List<Result>();
        
        var httpString = new Uri(
            $"https://www.willys.se/search/campaigns/offline?page=0&q={req.StoreId}&size=250&type=PERSONAL_GENERAL&avoidCache=1708079169569",
            UriKind.Absolute);
        var result =
            await _httpClient.GetAsync(httpString
                );
        if (!result.IsSuccessStatusCode)
        {
            return false;
        }

        string apiResponse = await result.Content.ReadAsStringAsync();
            WillysRoot? willysRoot = JsonSerializer.Deserialize<WillysRoot>(apiResponse);
            if (willysRoot.results.Count == willysRoot.pagination.pageSize)
            {
              //All products are not included
                return false;
            }
        productList.AddRange(willysRoot.results);
            
        foreach (var product in productList)
        {
           var productRecord = WillysToProductRecordMapper.Map(product);
           productRecord.Store = store;
           _webApiDbContext.ProductRecords.Add(productRecord);
        }
    await _webApiDbContext.SaveChangesAsync();
        return true;
    }
    
}


