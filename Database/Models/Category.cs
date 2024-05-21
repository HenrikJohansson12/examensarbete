namespace Database.Models;

public class Category
{
    public int Id { get; set; }
    public  string Name { get; set; }
    public IQueryable< ProductRecord> ProductRecords{ get; set; }
}