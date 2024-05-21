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
    public DbSet<Category> Categories { get; set; }
    public DbSet<Store> Stores { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<IngredientToRecipe> IngredientsToRecipes { get; set; }
    public DbSet<Ingredient> Ingredients { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);  // Make sure this line is called first
        
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

        var categories = new List<Category>
        {
            new Category() { Id = 1, Name = "Barnprodukter" },
            new Category() { Id = 2, Name = "Bröd och kakor" },
            new Category() { Id = 3, Name = "Chark/Delikatess" },
            new Category() { Id = 4, Name = "Dessert/Mellanmål" },
            new Category() { Id = 5, Name = "Djur" },
            new Category() { Id = 6, Name = "Drycker" },
            new Category() { Id = 7, Name = "Fisk och skaldjur" },
            new Category() { Id = 8, Name = "Frukt och bär" },
            new Category() { Id = 9, Name = "Färdigmat" },
            new Category() { Id = 10, Name = "Glass" },
            new Category() { Id = 11, Name = "Grönsaker" },
            new Category() { Id = 12, Name = "Hem och hushåll" },
            new Category() { Id = 13, Name = "Hälsa" },
            new Category() { Id = 14, Name = "Korv och pålägg" },
            new Category() { Id = 15, Name = "Kroppsvård" },
            new Category() { Id = 16, Name = "Kött" },
            new Category() { Id = 17, Name = "Mejeri" },
            new Category() { Id = 18, Name = "Ost" },
            new Category() { Id = 19, Name = "Skafferi" },
            new Category() { Id = 20, Name = "Kaffe" },
            new Category() { Id = 21, Name = "Snacks och godis" },
            new Category() { Id = 22, Name = "Vegetariskt" },
            new Category() { Id = 23, Name = "Övrigt" },
            
        };

        modelBuilder.Entity<Brand>().HasData(new List<Brand> { brand1, brand2 });
        modelBuilder.Entity<Store>().HasData(new List<Store> { store1, store2 });
        modelBuilder.Entity<Category>().HasData(categories);
    }
}


