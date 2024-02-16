namespace Database.Models;

public class Store
{
    public int Id { get; set; }
    public Address Address { get; set; }
    public string Name { get; set; }
    public Brand Brand { get; set; }
    public string InternalStoreId { get; set; }
    
}