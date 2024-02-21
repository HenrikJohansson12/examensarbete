using API.Properties.Services;
using Moq;
namespace BackEndTests;
using System.IO;
public class WillysServiceTest
{
    [Fact] 
    public void WillysJsonIsConvertedCorrectly()
    {
        string jsonContent = File.ReadAllText("willysresponse.json");
        //Mocka en http. 
        var mockHttpClient = new Mock<HttpClient>();
        mockHttpClient
            .Setup(client => client.GetStringAsync(It.IsAny<string>())) // Matchar alla strängargument
            .ReturnsAsync(jsonContent);
        
        
        var willysService = new WillysService(mockHttpClient.Object);
    
        willysService.GetDiscountedProducts();
        
        
        willysService.GetProductRecords();





    }
}