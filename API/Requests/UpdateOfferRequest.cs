using API.Models;

namespace API.Requests;

public class UpdateOfferRequest
{
    public List<UpdateOffersDto> Offers { get; set; }
}

public class UpdateOffersDto
{
    public  int ProductRecordId { get; set; }
    public  int CategoryId { get; set; }
    public  int IngredientId { get; set; }
}