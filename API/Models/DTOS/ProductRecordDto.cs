namespace API.Models.DTOS;

public class ProductRecordDto
{
        public int Id { get; set; }
        public string CountryOfOrigin { get; set; }
        public string? OfferType { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountedPrice { get; set; }
        public string Size { get; set; }
        public int  MinItems { get; set; }
        public int MaxItems { get; set; }
        public bool IsMemberOffer { get; set; }
        public string StoreName { get; set; }
        public string Category { get; set; }
        public double DiscountPercent { get; set; }
        
}