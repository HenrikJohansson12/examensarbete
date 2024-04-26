using System.Globalization;
using System.Net;
using Moq;
using System.IO;
using System.Text;
using System.Text.Unicode;
using API.Properties.Services;
using API.Requests;
using CsvHelper;
using Database;
using Database.Models;

namespace BackEndTests.IcaTest;

public class IcaServiceUnitTest
{
    public class IcaHttpMessageHandlerMock : HttpMessageHandler
    {
        private readonly HttpResponseMessage[] _responses;
        private int _count = 0;
        private readonly HttpResponseMessage _response;
        private readonly HttpStatusCode _code;
        
        public IcaHttpMessageHandlerMock(HttpStatusCode code)
        {
            _code = code;
        }
      
        public IcaHttpMessageHandlerMock( params HttpResponseMessage[] responses)
        {
            _responses = responses;
        }
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (_responses!= null)
            {
                var response = _responses[_count++ % _responses.Length];
                return Task.FromResult(response);

            }
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
    public async Task Ica_Products_Are_Correctly_Converted()
    {
        string firstResponse = File.ReadAllText("Icaresponse.json");
        string response2 = File.ReadAllText("Decorate1.json");
        string response3 = File.ReadAllText("Decorate2.json");
        string response4 = File.ReadAllText("Decorate3.json");
        string response5 = File.ReadAllText("Decorate4.json");
        var http = new HttpClient( new IcaHttpMessageHandlerMock(
            new HttpResponseMessage()
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(firstResponse)
        }, new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(response2),
            }, new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(response3),
            }, new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(response4),
            }, new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(response5),
            }
            
            ));
        var icaService = new IcaService(http, new WebApiDbContext());
        var request = new GetDiscountedItemsIcaRequest { StoreId = "" };
        // Test. 
        await  icaService.GetDiscountedProducts(request);

        var result = icaService.GetRecords();
        // Index 0
        var perKiloGramOffer = new ProductRecord()
        {
            OfferType = 2,
            Name = "Äpple Ingrid Marie ca 190g Klass 1 ICA",
            Brand = "ICA",
            Price = 35.0m,
            DiscountedPrice = 25.0m,
            Quantity = 190,
            QuantityUnit = "g",
            MinItems = 0,
            MaxItems = 0,
            IsMemberOffer = false,
            CountryOfOrigin = (int)(CountryOfOrigin.Sweden),
            //Not good for testing. 
            StartDate = new DateOnly(2024, 3, 11),
            EndDate = new DateOnly(2024, 3, 17),

        };
        // Index 2
        var multiOffer = new ProductRecord()
        {
            OfferType = 3,
            Name = "Mellanmjölkdryck 1,5% Laktosfri 1,5l Arla Ko®",
            Brand = "Arla Ko",
            Price = 21.90m,
            DiscountedPrice = 16.67m,
            Quantity = 1.5m,
            QuantityUnit = "l",
            MinItems = 3,
            MaxItems = 1,
            IsMemberOffer = false,
            CountryOfOrigin = (int)(CountryOfOrigin.Sweden),
            StartDate = new DateOnly(2024, 3, 11),
            EndDate = new DateOnly(2024, 3, 17),

        };
        // Index 4
        var perProductOffer = new ProductRecord()
        {
            OfferType = 1,
            Name = "Röd spetsig paprika 200g Klass 1 ICA",
            Brand = "ICA",
            Price = 24.90m,
            DiscountedPrice = 20m,
            Quantity = 0.2m,
            QuantityUnit = "kg",
            MinItems = 0,
            MaxItems = 0,
            IsMemberOffer = false,
            CountryOfOrigin = (int)(CountryOfOrigin.Spain),
            StartDate = new DateOnly(2024, 3, 11),
            EndDate = new DateOnly(2024, 3, 17),

        };
        
        var multiOffer2 = new ProductRecord()
        {
            OfferType = 3,
            Name = "Skinka Basturökt 120g Lönneberga",
            Brand = "Lönneberga",
            Price = 29.90m,
            DiscountedPrice = 20m,
            Quantity = 0.12m,
            QuantityUnit = "kg",
            MinItems = 2,
            MaxItems = 0,
            IsMemberOffer = true,
            CountryOfOrigin = (int)(CountryOfOrigin.Sweden),
            StartDate = new DateOnly(2024, 3, 11),
            EndDate = new DateOnly(2024, 3, 17),

        };

        Assert.Equivalent(perKiloGramOffer,result[0]);
        Assert.Equivalent(multiOffer,result[2]);
        Assert.Equivalent(perProductOffer,result[4]);
        Assert.Equivalent(multiOffer2,result[28]);
        
        
    }

    [Fact]
    public async void Convert_Ica_Data_To_Csv()
    {
        var httpClient = new HttpClient();
        var icaservice = new IcaService(httpClient, new WebApiDbContext());
        var req = new GetDiscountedItemsIcaRequest{StoreId = "1004101"};

       await icaservice.GetDiscountedProducts(req);

       var result = icaservice.GetRecords();
    
       using (var writer = new StreamWriter(@"C:\dev\examensarbete\exports\icatest17.csv", false,
                  Encoding.UTF32))
           
       using (var csv = new CsvWriter(writer, new CultureInfo("se-SE")))
       {
           csv.WriteRecords(result);
       }

    }
    
}


    
    
   
