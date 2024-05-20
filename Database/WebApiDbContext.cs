using System.Text.Json;
using Database.Models;
using Database.Models.Livsmedelsverket;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Database;

public class WebApiDbContext : IdentityDbContext<IdentityUser>
{
    public WebApiDbContext(DbContextOptions<WebApiDbContext> options)
        : base(options)
    {
    }

    public DbSet<ProductRecord> ProductRecords { get; set; }
    public DbSet<Store> Stores { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<IngredientToRecipe> IngredientsToRecipes { get; set; }
    public DbSet<Ingredient> Ingredients { get; set; }
    


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        modelBuilder.Entity<Recipe>()
            .HasMany(r => r.Ingredients)
            .WithOne(ir => ir.Recipe)
            .HasForeignKey(ir => ir.RecipeId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<IngredientToRecipe>()
            .HasOne(ir => ir.Ingredient)
            .WithMany()
            .HasForeignKey(ir => ir.IngredientId);

        modelBuilder.Entity<IngredientToRecipe>()
            .HasOne(ir => ir.Recipe)
            .WithMany(r => r.Ingredients)
            .HasForeignKey(ir => ir.RecipeId);

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

