using System.Globalization;
using System.Text.Json;
using System.Text.RegularExpressions;
using API.Models;
using Database.Models;

namespace API.Properties.Services;

public class WillysService:IStoreService
{
    public async void GetDiscountedProducts()
    {
        var productList = new List<Result>();
        using (HttpClient httpClient = new HttpClient())
        {
            string apiResponse =
                await httpClient.GetStringAsync("https://www.willys.se/search/campaigns/offline?page=0&q=2103&size=250&type=PERSONAL_GENERAL&avoidCache=1708079169569");
            WillysRoot? willysRoot = JsonSerializer.Deserialize<WillysRoot>(apiResponse);

            if (willysRoot.results.Count == willysRoot.pagination.pageSize)
            {
                Console.WriteLine("Alla produkter är INTE med");
            }
            
            productList.AddRange(willysRoot.results);
        }

        var productRecordList = new List<ProductRecord>();
        
        foreach (var product in productList)
        {
            productRecordList.Add(CreateProductRecord(product));
        }

        Console.Read();

    }
    
    private ProductRecord CreateProductRecord(Result result)
    {   
       
        string format = "dd/MM-yyyy";
        CultureInfo provider = CultureInfo.InvariantCulture;
        DateOnly startDate;
        DateOnly.TryParseExact(result.potentialPromotions.FirstOrDefault().startDate, format, provider,
            DateTimeStyles.None, out startDate);
        DateOnly endDate;
        DateOnly.TryParseExact(result.potentialPromotions.FirstOrDefault().endDate, format, provider,
            DateTimeStyles.None, out endDate);

        
        bool isMemberOffer=false;
        int quantity = 0;
        string unit = "";
        var pattern = @"\d+";
        Match quantityMatch = Regex.Match(result.displayVolume, pattern);
        if (quantityMatch.Success)
        {
            quantity = int.Parse(quantityMatch.Value);
        }
        
        Match match = Regex.Match(result.displayVolume, @"(\d+)(\D+)");
        if (match.Success)
        {
             unit = match.Groups[2].Value;
            
        }

        int minItems = 0;
        if (result.potentialPromotions.FirstOrDefault().realMixAndMatch)
        {
            minItems = result.potentialPromotions.FirstOrDefault().qualifyingCount.Value;
        }
        if (result.potentialPromotions.FirstOrDefault().campaignType == "LOYALTY")
        {
            isMemberOffer = true;
        }
        var productRecord = new ProductRecord()
        {
            Name = result.name,
            Brand = result.manufacturer,
            Quantity = quantity,
            QuantityUnit = unit,
            Price = Convert.ToDecimal(result.price),
            DiscountedPrice = Convert.ToDecimal(result.potentialPromotions[0].price),
            MinItems = minItems,
            MaxItems = Convert.ToInt32(result.potentialPromotions[0].redeemLimit),
            IsMemberOffer = isMemberOffer,
            StartDate = startDate,
            EndDate = endDate,
        };
        return productRecord;
    }
}