using Database.Models;
using Database.Models.DTO;

namespace API.Responses;

public class GetTop10MostDiscountedProductsResponse
{
    public List<ProductRecordDTO> Result { get; set; }
}