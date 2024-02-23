using System.Net;
using API.Properties.Services;
using API.Requests;
using Moq;
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
    public  void WillysJsonIsConvertedCorrectly()
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
      service.GetDiscountedProducts(req);

      var result = service.GetProductRecords();
      
      Assert.NotNull(result);
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
