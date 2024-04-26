using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Database;

public class WebApiDbContext : DbContext
{
    public DbSet<ProductRecord> ProductRecords { get; set; }
    public DbSet<Store> Stores { get; set; }
    public DbSet<Brand> Brands { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("WebApiDatabase");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var brand1 = new Brand { Id = 1, Name = "Ica" };
        var brand2 = new Brand { Id = 2, Name = "Willys" };

        var store1 = new Store
        {
            Id = 1,
            Name = "Willys Knalleland",
            InternalStoreId = "2110",
            StreetAddress = "Knallelandsvägen 3",
            ZipCode = "50637",
            City = "Borås",
            BrandId = brand2.Id
        };

        var store2 = new Store
        {
            Id = 2,
            Name = "Ica City Knalleland",
            InternalStoreId = "1004101",
            StreetAddress = "Knallelandsvägen 12",
            ZipCode = "50637",
            City = "Borås",
            BrandId = brand1.Id
        };

        modelBuilder.Entity<Brand>().HasData(new List<Brand> { brand1, brand2 });
        modelBuilder.Entity<Store>().HasData(new List<Store> { store1, store2 });

        
        base.OnModelCreating(modelBuilder);
        
    }
}
