using API.Models;

namespace API.Responses;

public class GetUnmappedOffersResponse
{
    public List<MapOfferDto> Offers { get; set; }
}