
using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Database;

public class WebApiDbContext : DbContext
{
    public DbSet<ProductRecord> ProductRecords { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=webapi.db");
    }
}

