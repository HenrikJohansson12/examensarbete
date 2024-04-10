using System.Globalization;
using System.Net;
using System.Text;
using API.Properties.Services;
using API.Requests;
using CsvHelper;
using Database.Models;
namespace BackEndTests;
using System.IO;
public class WillysServiceTest
{
    public class HttpMessageHandlerMock : HttpMessageHandler
    {
        private readonly HttpStatusCode _code;
        private readonly HttpResponseMessage _response;
        public HttpMessageHandlerMock(HttpStatusCode code)
        {
            _code = code;
        }
        
        public HttpMessageHandlerMock(HttpResponseMessage response)
        {
            _response = response;
        }
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (_response!= null)
            {
                return Task.FromResult(_response);

            }
            return Task.FromResult(new HttpResponseMessage()
            {
                        StatusCode = _code,
            });
        }
    }
    [Fact] 
    public async void WillysJsonIsConvertedCorrectly()
    {
        string jsonContent = File.ReadAllText("willysresponse.json");
        
        var req = new GetDiscountedItemsWillysRequest
        {
            StoreId = 1
        };
        var http = new HttpClient( new HttpMessageHandlerMock(new HttpResponseMessage()
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(jsonContent)
        }));
        var service = new WillysService(http);
    await service.GetDiscountedProducts(req);

      var result = service.GetProductRecords();

      var perKiloGramOffer = new ProductRecord()
      {
          OfferType = 2,
          Name = "Färsk fläskfilé",
          Brand = "Danish Crown",
          Price = 129.0m,
          DiscountedPrice = 69.9m,
          Quantity = 500,
          QuantityUnit = "g",
          MinItems = 0,
          MaxItems = 3,
          IsMemberOffer = true,
          StartDate = new DateOnly(2024, 2, 19),
          EndDate = new DateOnly(2024, 2, 25),
          CountryOfOrigin = 0

      };
      var multiOffer = new ProductRecord()
      {
          OfferType = 3,
          Name = "Flytande tvättmedel, sköljmedel",
          Brand = "Grumme",
          Price = 36.9m,
          DiscountedPrice = 25m,
          Quantity = 750,
          QuantityUnit = "ml",
          MinItems = 2,
          MaxItems = 2,
          IsMemberOffer = true,
          StartDate = new DateOnly(2024, 2, 19),
          EndDate = new DateOnly(2024, 2, 25),
          CountryOfOrigin = 0

      };
      
      var perProductOffer = new ProductRecord()
      {
          OfferType = 1,
          Name = "Grönkål",
          Brand = "Spanien",
          Price = 29.9m,
          DiscountedPrice = 19.9m,
          Quantity = 200,
          QuantityUnit = "g",
          MinItems = 0,
          MaxItems = 0,
          IsMemberOffer = false,
          StartDate = new DateOnly(2024, 2, 19),
          EndDate = new DateOnly(2024, 2, 25),
          CountryOfOrigin = 0

      };
      
      Assert.Equivalent(perKiloGramOffer,result[0]);
      Assert.Equivalent(multiOffer,result[2]);
      Assert.Equivalent(perProductOffer,result[27]);
    }
    
    [Fact]
  public async Task Returns_Null_When_400()
  {
      var http = new HttpClient( new HttpMessageHandlerMock(HttpStatusCode.BadRequest));
      var service = new Service(http);
      var result = await service.CreateAsync();
      Assert.Null(result);

  }
  
  [Fact]
  public async Task Returns_Json_When_200()
  {
      string jsonContent = File.ReadAllText("willysresponse.json");
      var http = new HttpClient( new HttpMessageHandlerMock(new HttpResponseMessage()
      {
          StatusCode = HttpStatusCode.OK,
          Content = new StringContent(jsonContent)
      }));
      var service = new Service(http);
      var result = await service.CreateAsync();
      Assert.NotNull(result);
      Assert.Equal(jsonContent,result);

  }
  


[Fact]
public async void Convert_Willys_Data_To_Csv()
{
    var httpClient = new HttpClient();
    var willysService = new WillysService(httpClient);
    var req = new GetDiscountedItemsWillysRequest(){StoreId = 123};

    await willysService.GetDiscountedProducts(req);

    var result = willysService.GetProductRecords();
 
    using (var writer = new StreamWriter(@"C:\dev\examensarbete\exports\willystest.csv", false,
               Encoding.UTF32))
           
    using (var csv = new CsvWriter(writer, new CultureInfo("se-SE")))
    {
        csv.WriteRecords(result);
    }

}
}



public class Service
{
    private readonly HttpClient _http;

    public Service(HttpClient http)
    {
        _http = http;
    }
    public async Task<string> CreateAsync()
    {
        var response = await _http.GetAsync("https://google.com");
        if (response.StatusCode == HttpStatusCode.BadRequest)
        {
            return null;
        }

        return await response.Content.ReadAsStringAsync();
    }
}
