using Database.Models;

namespace API.Responses;

public class GetTop10MostDiscountedProductsResponse
{
    public List<ProductRecord> Result { get; set; }
}