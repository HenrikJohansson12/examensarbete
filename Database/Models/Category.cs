namespace Database.Models;

public class Category
{
    public int Id { get; set; }
    public  string Name { get; set; }
    public IEnumerable< ProductRecord> ProductRecords{ get; set; }
}