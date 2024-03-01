namespace BackEndTests;

public class IcaServiceUnitTest
{
    [Fact]
    public async void IcaJsonIsConvertedCorrectly()
    {
        var mockHttp = new MockHttpMessageHandler();

        var request = mockHttp.When("http://localhost/api/user/*")
            .Respond("application/json", "{'name' : 'Test McGee'}");

        var client = mockHttp.ToHttpClient();

        await client.GetAsync("http://localhost/api/user/1234");
        await client.GetAsync("http://localhost/api/user/2345");
        await client.GetAsync("http://localhost/api/user/3456");

        Console.Write(mockHttp.GetMatchCount(request)); // 3
    }
}