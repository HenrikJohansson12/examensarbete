namespace Database.Models;

public class Store
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Brand Brand { get; set; }
    public int BrandId { get; set; }
    public string InternalStoreId { get; set; }
    public string StreetAddress { get; set; }
    public string ZipCode { get; set; }
    public string City { get; set; }
}