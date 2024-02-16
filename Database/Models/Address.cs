namespace Database.Models;

public class Address
{
    public int Id { get; set; }
    public string StreetAddress { get; set; }
    public int StreetNumber { get; set; }
    public string ZipCode { get; set; }
    public string City { get; set; }
}