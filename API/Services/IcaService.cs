using System.Data.SqlTypes;
using System.Globalization;
using API.Models;
using System.Text.Json;
using System.Text.RegularExpressions;
using API.Mappers;
using API.Requests;
using Castle.Components.DictionaryAdapter.Xml;
using Database;
using Database.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Newtonsoft.Json.Linq;

namespace API.Properties.Services;

public interface IIcaService
{
    Task<bool> GetDiscountedProducts(GetDiscountedItemsIcaRequest req);
}

public class IcaService : IIcaService
{
    private WebApiDbContext _dbContext;
    private readonly HttpClient _httpClient;
    public static List<ProductRecord> productRecords = new();

    public IcaService(HttpClient httpClient, WebApiDbContext dbContext)
    {
        _httpClient = httpClient;
        _dbContext = dbContext;
    }

    public List<ProductRecord> GetRecords()
    {
        return productRecords;
    }

    public async Task<bool> GetDiscountedProducts(GetDiscountedItemsIcaRequest req)
    {
        var productList = new List<Product>();

        var httpString1 = new Uri($"https://handlaprivatkund.ica.se/stores/{req.StoreId}/api/v5/products/",
            UriKind.Absolute);

        var result =
            await _httpClient.GetAsync(httpString1
            );
        if (!result.IsSuccessStatusCode)
        {
            return false;
        }

        var icaRoot = JsonSerializer.Deserialize<IcaRoot>(await result.Content.ReadAsStringAsync());
        if (icaRoot is null)
        {
            return false;
        }


        var productIds = new List<string>();
        //Hämtar ut alla  som har on offer typen. 
        //När den inte har on offer type så finns ingen prishistorik med. 
        foreach (var types in icaRoot.result.productGroups)
        {
            if (types.type == "on_offer")
            {
                productIds.AddRange(types.products);
            }
        }

        //Split the list of product Id's since the endpoint allows a few at the time.  
        List<List<string>> dividedLists = new List<List<string>>();
        foreach (string[] chunk in productIds.Chunk(10))
        {
            dividedLists.Add(chunk.ToList());
        }

        string decorateProductIds = "";
        for (int i = 0; i < dividedLists.Count; i++)
        {
            for (int j = 0; j < dividedLists[i].Count; j++)
            {
                if (j == 0)
                {
                    decorateProductIds = dividedLists[i][j] + ",";
                }

                else
                {
                    decorateProductIds = decorateProductIds + dividedLists[i][j] + ",";
                }
            }

            var productResponse = await _httpClient.GetStringAsync(
                $"https://handlaprivatkund.ica.se/stores/{req.StoreId}/api/v5/products/decorate?productIds=" +
                decorateProductIds);
            IcaProducts? icaProducts =
                JsonSerializer.Deserialize<IcaProducts>(productResponse);
            if (icaProducts != null)
                if (icaProducts.products != null)
                    productList.AddRange(icaProducts.products);
        }

        foreach (var product in productList)
        {
            var productRecord = IcaToProductRecordMapper.Map(product);
            productRecords.Add(productRecord);
            _dbContext.ProductRecords.Add(productRecord);
            await _dbContext.SaveChangesAsync();
        }

        return true;
    }
}