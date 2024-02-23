using System.Data.SqlTypes;
using API.Models;
using System.Text.Json;
using System.Text.RegularExpressions;
using Database.Models;
using Newtonsoft.Json.Linq;

namespace API.Properties.Services;

public interface IIcaService
{
    
}
public class IcaService : IIcaService
{
    private readonly HttpClient _httpClient;
    public static List<ProductRecord> productRecords = new();
    public IcaService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<bool> GetDiscountedProducts()
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

            //Delar upp den i mindre delar då decorateEndpointen har en begränsning på antal produkter man får hämta. 
            List<List<string>> dividedLists = DivideList(productIds, 10);
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
                productList.AddRange(icaProducts.products);
            }

            var productRecordList = new List<ProductRecord>();

            foreach (var product in productList)
            {
                productRecords.Add(CreateProductRecord(product));
            }
            
            Console.WriteLine("ss");
            //Todo normalisera datat mellan willys och ica och spara i en databas. 
            return true;
    }

        //todo Kolla så den här metoden verkligen fungerar. 
        static List<List<T>> DivideList<T>(List<T> sourceList, int chunkSize)
        {
            var dividedLists = new List<List<T>>();

            // Beräkna antalet delar vi behöver
            int numberOfChunks = (int)Math.Ceiling((double)sourceList.Count / chunkSize);

            // Loopa genom listan och skapa nya delade listor
            for (int i = 0; i < numberOfChunks; i++)
            {
                dividedLists.Add(sourceList.Skip(i * chunkSize).Take(chunkSize).ToList());
            }
            return dividedLists;
        }
    

    private ProductRecord CreateProductRecord(Product product)
    {
        //Om priset är per kilo så blir priserna konstiga. Sätter vi en bool
        //kanske vi kan reda ut det bättre. 
        
        // Kanske man ska skapa en enum som håller de olika erbjudande typerna som finns. 
        //Det finns per styck, 3 för 30 kr, lösvikt i kg. Detta använder frontend för att presentera
        //Datan tydligare. 
        bool isKgPrice = product.offer.description.Contains("kr/kg");
        
        string name = product.name;
        string brand = product.brand;
        string description = product.name; // Kanske får trixa lite med denna
        decimal price = 0;
        if (product.price.original==null)
        {
            price = 0;
        }
        else
        {
            price = Convert.ToDecimal(product.price.original.amount);
        }
        
        int minItems = 0;
        decimal discountedPrice = 0;
        
        if (product.price.current == null)
        {
            //Gör nåt med regex. T.ex 2 för 30.
            Regex regex = new Regex(@"(\d+)\s*för\s*(\d+)");
            Match match2 = regex.Match(product.offer.description);
            if (match2.Success)
            {
                minItems = Convert.ToInt32(match2.Groups[1]);
                discountedPrice = Convert.ToDecimal(match2.Groups[2].Value)/minItems;
            }
        }
        else
        {
            discountedPrice = Convert.ToDecimal(product.price.current.amount);
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
        
        DateTime date = DateTime.Now; // Replace with your date

        // Calculate start (Monday) of the week
        int diffToMonday = date.DayOfWeek - DayOfWeek.Monday;
        if (diffToMonday < 0)
        {
            diffToMonday += 7;
        }
        
        DateTime startDate = date.AddDays(-1 * diffToMonday);

        // Calculate end (Sunday) of the week
        DateTime endDate = startDate.AddDays(6);

        
        var productRecord = new ProductRecord()
        {
            Name = name,
            Brand = brand,
            Description = description,
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
 

