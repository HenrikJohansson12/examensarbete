namespace Database.Models;

public class Brand
{
    public int Id { get; set; }
    public string Name { get; set; }

    public ICollection<Store> Stores { get; set; }
}