using System.Data.SqlTypes;
using API.Models;
using System.Text.Json;
using System.Text.RegularExpressions;
using API.Requests;
using Castle.Components.DictionaryAdapter.Xml;
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
    private readonly HttpClient _httpClient;
    public static List<ProductRecord> productRecords = new();
    public IcaService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<bool> GetDiscountedProducts(GetDiscountedItemsIcaRequest req)
    {
        var productList = new List<Product>();
            //Hämtar json strängen
            string apiResponse =
                await _httpClient.GetStringAsync("https://handlaprivatkund.ica.se/stores/1004101/api/v5/products/");

            //Deserialiserar den till objekt. 
            IcaRoot? icaRoot =
                JsonSerializer.Deserialize<IcaRoot>(apiResponse);

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

                    decorateProductIds = decorateProductIds + dividedLists[i][j] + ",";
                }

                string productResponse = await _httpClient.GetStringAsync(
                    "https://handlaprivatkund.ica.se/stores/1004101/api/v5/products/decorate?productIds=" +
                    decorateProductIds);
                IcaProducts? icaProducts =
                    JsonSerializer.Deserialize<IcaProducts>(productResponse);
                if (icaProducts != null)
                    if (icaProducts.products != null)
                        productList.AddRange(icaProducts.products);
            }
            
            foreach (var product in productList)
            {
                productRecords.Add(CreateProductRecord(product));
            }
            
            return true;
    }
    
    private ProductRecord CreateProductRecord(Product product)
    {
        int offerType;
        
        if (product.offer.requiredProductQuantity!=null)
        {
            offerType = (int)(OfferType.MultiBuyOffer);
        }

       else if (product.offer.description.Contains("st"))
        {
            offerType = (int)(OfferType.PerProduct);
        }

       else
       {
           offerType = (int)(OfferType.PerKiloGram);
       }
       
        
        string name = product.name;
        string brand = product.brand;

        decimal price;
        if (decimal.TryParse(product.price.original?.amount, out price)== false)
        {
            price = 0;
        }
        
        var minItems = 0;
        decimal discountedPrice = 0;
        //Finding the discounted price based on the offertype. 
        switch (offerType)
        {   //Per product
            case 1:
                discountedPrice = Convert.ToDecimal(product.price.current.amount);
                break;
            //Per kg
            case 2:
                string perkgPattern = @"(\d+)\s*kr";
                Match perKgMatch = Regex.Match(product.offer.description,perkgPattern);
                if (perKgMatch.Success)
                {
                    discountedPrice = decimal.Parse(perKgMatch.Groups[1].Value);
                }
                break;
            //MultiOffer
            case 3:
                string multiOfferPattern = @"för\s+(\d+)\s+kr";
                Match multiOfferMatch = Regex.Match(product.offer.description,multiOfferPattern);
                if (multiOfferMatch.Success)
                {
                    discountedPrice = decimal.Parse(multiOfferMatch.Groups[1].Value);
                }
                break;
        }
        
        decimal quantity = 0;
        string quantityUnit = "";
        string pattern = @"(\d+([.,]\d+)?)(.*)";
        Match match = Regex.Match(product.size.value, pattern);
        if (match.Success)
        {
            quantity = decimal.Parse(match.Groups[1].Value.Replace(',', '.'));
            quantityUnit = match.Groups[3].Value.Trim();
        }
        
        //Ibland kan vara en min items kampanj men variabeln är inte satt. 
        if (product.offer.requiredProductQuantity!= null)
        {
            minItems = product.offer.requiredProductQuantity.Value;
        }

        bool isMemberOffer = product.offer.description.Contains("Stammis");
        int maxItems = 0;

        if (product.offer.description.Contains("Max"))
        {
            Regex regex = new Regex(@"Max\s*(\d+)");
            Match maxMatch = regex.Match(product.offer.description);
            if (maxMatch.Success)
            {
                maxItems = Convert.ToInt32(maxMatch.Groups[1].Value);
            }
        }
        
        DateTime date = DateTime.Now; 
        
        int diffToMonday = date.DayOfWeek - DayOfWeek.Monday;
        if (diffToMonday < 0)
        {
            diffToMonday += 7;
        }
        
        DateTime startDate = date.AddDays(-1 * diffToMonday);

       
        DateTime endDate = startDate.AddDays(6);
        
        var productRecord = new ProductRecord()
        {
            Name = name,
            OfferType = offerType,
            Brand = brand,
            Description = "",
            Price = price,
            DiscountedPrice = discountedPrice,
            Quantity = quantity,
            QuantityUnit = quantityUnit,
            MinItems = minItems,
            MaxItems = maxItems,
            IsMemberOffer = isMemberOffer,
            StartDate = DateOnly.FromDateTime(startDate),
            EndDate = DateOnly.FromDateTime(endDate),
        };
        
        return productRecord;

        /* public List<Product> ExtractProductInformation(string response)
        {
            throw new NotImplementedException();
        }
        */
    }
    
}
 

