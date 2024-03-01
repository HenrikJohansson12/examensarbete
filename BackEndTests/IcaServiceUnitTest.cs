using System.Net;
using Moq;
namespace BackEndTests;

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
    public async Task Returns_JsonSequence_When_200()
    {
        string jsonContent = File.ReadAllText("willysresponse.json");
        var http = new HttpClient( new IcaHttpMessageHandlerMock(
            new HttpResponseMessage()
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent("{\"boo\": 42}")
        }, new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("{\"foo\": 41}"),
            }
            ));
        var service = new ServiceTest(http);
        var result = await service.CreateAsync();
        
        Assert.NotNull(result);
        Assert.Equal("{\"boo\": 42}",result);
        
        result = await service.CreateAsync();
        
        Assert.NotNull(result);
        Assert.Equal("{\"foo\": 41}",result);

    }
  
}

public class ServiceTest
{
    private readonly HttpClient _http;

    public ServiceTest(HttpClient http)
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
    
    
   
