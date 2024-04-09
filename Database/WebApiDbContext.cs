
using System.Globalization;
using CsvHelper;
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
        using (var reader = new StreamReader("C:\\dev\\examensarbete\\exports\\icatestweek11.csv"))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            var productRecords = csv.GetRecords<ProductRecord>();
            modelBuilder.Entity<ProductRecord>().HasData(productRecords);
        }

        modelBuilder.Entity<Brand>().HasData(
            new Brand() { Id = 1, Name = "Ica" },
            new Brand { Id = 2, Name = "Willys" }
            // Add more products here
        );
        modelBuilder.Entity<Store>().HasData(
            new Store
            {
                Name = "Willys Knalleland",
                InternalStoreId = "2103",
                Brand = new Brand{Id = 2,Name = "Willys"}
                
            },
        new Store
            {
                Name = "Ica City Knalleland",
                InternalStoreId = "1004101",
                Brand = new Brand{Id = 1,Name = "Ica"}
            }
        );
    }
    
}

