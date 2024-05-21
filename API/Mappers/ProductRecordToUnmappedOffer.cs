using API.Models;
using Database.Models;

namespace API.Mappers;

public class ProductRecordToUnmappedOffer
{
    public static MapOfferDto To(ProductRecord productRecord)
    {
        return new MapOfferDto()
        {
            Id = productRecord.Id,
            Brand = productRecord.Brand,
            Description = productRecord.Description,
            Name = productRecord.Name
        };
    }
}