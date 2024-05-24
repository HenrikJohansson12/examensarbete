using API.Models.DTOS;
using Database.Models;
namespace API.Mappers;

public class ProductRecordToDTO
{
    public static ProductRecordDto To(ProductRecord productRecord)
    {
        var dto = new ProductRecordDto()
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
            IsMemberOffer = productRecord.IsMemberOffer,
            StoreName = productRecord.Store.Name,
            DiscountPercent =(double) ((productRecord.Price - productRecord.DiscountedPrice) / productRecord.Price)*100,
            Category = productRecord.Category.Name
            
        };
        return dto;
    }
}