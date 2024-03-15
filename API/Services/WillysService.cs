using System.Globalization;
using System.Text.Json;
using System.Text.RegularExpressions;
using API.Models;
using API.Requests;
using Database.Models;

namespace API.Properties.Services;

public interface IWillysService
{
    Task<bool> GetDiscountedProducts(GetDiscountedItemsWillysRequest req);
}
public class WillysService : IWillysService
{
    private readonly HttpClient _httpClient;
    private List<ProductRecord> _productRecords = new List<ProductRecord>();
    public WillysService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public List<ProductRecord> GetProductRecords()
    {
        return _productRecords;
    }
    public async Task<bool> GetDiscountedProducts(GetDiscountedItemsWillysRequest req)
    {
        var productList = new List<Result>();

        var result =
            await _httpClient.GetAsync(
                "https://www.willys.se/search/campaigns/offline?page=0&q=2103&size=250&type=PERSONAL_GENERAL&avoidCache=1708079169569");
        if (!result.IsSuccessStatusCode)
        {
            return false;
        }

        string apiResponse = await result.Content.ReadAsStringAsync();
            WillysRoot? willysRoot = JsonSerializer.Deserialize<WillysRoot>(apiResponse);
            if (willysRoot.results.Count == willysRoot.pagination.pageSize)
            {
                Console.WriteLine("Alla produkter är INTE med");
                return false;
            }
        
        productList.AddRange(willysRoot.results);
            
        foreach (var product in productList)
        {
            _productRecords.Add(CreateProductRecord(product));
        }
     Console.WriteLine();   
        return true;
    }

    private ProductRecord CreateProductRecord(Result result)
    {
        try
        {
            int offerType;
            //First we need to determine what kind of offer it is. 
            if (result.potentialPromotions.FirstOrDefault().realMixAndMatch)
            {
                offerType = (int)(OfferType.MultiBuyOffer);
            }
            
            else if (result.priceUnit == "kr/kg")
            {
                offerType = (int)(OfferType.PerKiloGram);
            }

            else if (result.priceUnit == "kr/st")
            {
                offerType = (int)(OfferType.PerProduct);
            }

            else offerType = (int)(OfferType.None);
            
            int quantity = 0;
            string unit = "";
            Match quantityMatch = Regex.Match(result.displayVolume, @"\d+");
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

            int maxItems = 0;

            var redeemLimit = result.potentialPromotions.FirstOrDefault().redeemLimit;
            if (redeemLimit != null)
                maxItems = Convert.ToInt32(redeemLimit.Value);

            bool isMemberOffer = false;
            if (result.potentialPromotions.FirstOrDefault().campaignType == "LOYALTY")
            {
                isMemberOffer = true;
            }

            //Parse the dates. 
            string format = "dd/MM-yyyy";
            CultureInfo provider = CultureInfo.InvariantCulture;
            DateOnly startDate;
            DateOnly.TryParseExact(result.potentialPromotions.FirstOrDefault().startDate, format, provider,
                DateTimeStyles.None, out startDate);
            DateOnly endDate;
            DateOnly.TryParseExact(result.potentialPromotions.FirstOrDefault().endDate, format, provider,
                DateTimeStyles.None, out endDate);
            decimal price;
            price = Convert.ToDecimal(result.price, new CultureInfo("en-US"));
                 
            var discountedPrice = 0m;

            if (result.potentialPromotions.FirstOrDefault().price == null)
            {
                Match priceMatch = Regex.Match(result.potentialPromotions.FirstOrDefault().rewardLabel,@"(\d+,\d+)" );
                if (priceMatch.Success)
                {    
                    string numberString = priceMatch.Groups[1].Value; 
                    numberString = numberString.Replace(",", ".");
                    discountedPrice = Convert.ToDecimal(numberString, new CultureInfo("en-US"));
                   
                }

            }
            else discountedPrice = Convert.ToDecimal( result.potentialPromotions.FirstOrDefault().price.Value);
            
            if (offerType == 3) //MultiBuyOffer
            {
                discountedPrice /= minItems;
            }
            
            var countryOfOrigin = 0;
            if (result?.labels == null)
            {
                countryOfOrigin = (int)(CountryOfOrigin.Unknown);
            }
            else
            {
              var swedishOrigin =  result?.labels?.Any(x => x.Contains("swedish_flag") || x.Contains("meat_from_sweden"));
              if (swedishOrigin == true)
              {
                  countryOfOrigin = (int)(CountryOfOrigin.Sweden);
              }
            }
            
            
            var productRecord = new ProductRecord()
            {
                OfferType = offerType,
                Name = result.name,
                Brand = result.manufacturer,
                Quantity = quantity,
                QuantityUnit = unit,
                Price = price,
                DiscountedPrice = discountedPrice,
                MinItems = minItems,
                MaxItems = maxItems,
                CountryOfOrigin = countryOfOrigin,
                IsMemberOffer = isMemberOffer,
                StartDate = startDate,
                EndDate = endDate,
            };
            return productRecord;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}


