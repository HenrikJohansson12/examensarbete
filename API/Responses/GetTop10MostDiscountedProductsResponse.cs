using API.Models.DTOS;
namespace API.Responses;

public class GetTop10MostDiscountedProductsResponse
{
    public List<ProductRecordDto> Products { get; set; }
}