using API.Models;
using System.Text.Json;
using System.Text.RegularExpressions;
using Database.Models;
using Newtonsoft.Json.Linq;

namespace API.Properties.Services;



public class IcaService : IStoreService
{
    public async void GetDiscountedProducts()
    {
    
        using (HttpClient httpClient = new HttpClient())
        {
            var productList = new List<Product>();
            //Hämtar json strängen
            string apiResponse =
                await httpClient.GetStringAsync("https://handlaprivatkund.ica.se/stores/1004101/api/v5/products/");
            
            //Deserialiserar den till objekt. 
                IcaRoot? icaRoot = 
                    JsonSerializer.Deserialize<IcaRoot>(apiResponse);
                
                var productIds = new List<string>();
                //Hämtar ut alla  som har on offer typen. 
                //När den inte har on offer type så finns ingen prishistorik med. 
                foreach (var types in icaRoot.result.productGroups)
                {
                    if (types.type =="on_offer")
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
                        if (j==0)
                        {
                            decorateProductIds = dividedLists[i][j]+",";
                        }

                        decorateProductIds = decorateProductIds +   dividedLists[i][j]+",";
                    }
                    
                    string productResponse = await httpClient.GetStringAsync("https://handlaprivatkund.ica.se/stores/1004101/api/v5/products/decorate?productIds="+decorateProductIds);
                 IcaProducts?  icaProducts =   
                       JsonSerializer.Deserialize<IcaProducts>(productResponse);
                productList.AddRange(icaProducts.products);
                }
                
                Console.WriteLine();
            //Todo normalisera datat mellan willys och ica och spara i en databas. 
               
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
    }

    private ProductRecord CreateProductRecord(Product product)
    {
        string name = product.name;
        string brand = product.brand;
        string description = product.name; // Kanske får trixa lite med denna
        decimal price = Convert.ToDecimal(product.price.original.amount);
        decimal discountedPrice = Convert.ToDecimal(product.price.current.amount);
        decimal quantity =0;
        string quantityUnit ="";
        string pattern = @"(\d+([.,]\d+)?)(.*)";
        Match match = Regex.Match(product.size.value, pattern);
        if (match.Success)
        {
             quantity = decimal.Parse(match.Groups[1].Value.Replace(',', '.'));
             quantityUnit = match.Groups[3].Value.Trim();
        }

        int maxItems;
        


    /* public List<Product> ExtractProductInformation(string response)
    {
        throw new NotImplementedException();
    }
    */
}
 

