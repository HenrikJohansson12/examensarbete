using System.Runtime.InteropServices.JavaScript;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Database.Models;

public class ProductRecord
{
    public int Id { get; set; }
    public int CountryOfOrigin { get; set; }
    public int OfferType { get; set; }
    public string Name { get; set; }
    public string Brand { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public decimal DiscountedPrice { get; set; }
    public decimal Quantity { get; set; }
    public string QuantityUnit { get; set; }
    public int  MinItems { get; set; }
    public int MaxItems { get; set; }
    public bool IsMemberOffer { get; set; }
    public int StoreId { get; set; }
    public Store Store { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }

    
    //Hade varit nice att hämta bilden och spara nånstans?
}