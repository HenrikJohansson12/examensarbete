using Database.Models;
using Database.Models.DTO;

namespace API.Mappers;

public class ProductRecordToDTO
{
    public static ProductRecordDTO To(ProductRecord productRecord)
    {
        var dto = new ProductRecordDTO()
        {
            Id = productRecord.Id,
            Brand = productRecord.Brand,
            OfferType = Enum.GetName(typeof(OfferType), productRecord.OfferType),
            CountryOfOrigin = Enum.GetName(typeof(CountryOfOrigin), productRecord.CountryOfOrigin),
            Name = productRecord.Name,
            Description = productRecord.Description?? "", 
            Price = productRecord.Price,
            DiscountedPrice = productRecord.DiscountedPrice,
            Size = productRecord.Quantity + " " + productRecord.QuantityUnit,
            MinItems = productRecord.MinItems,
            MaxItems = productRecord.MaxItems,
            IsMemberOffer = productRecord.IsMemberOffer
        };
        return dto;
    }
}