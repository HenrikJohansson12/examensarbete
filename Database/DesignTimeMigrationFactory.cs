using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Database
{
    public class WebApiDbContextFactory : IDesignTimeDbContextFactory<WebApiDbContext>
    {
        public WebApiDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<WebApiDbContext>();
            optionsBuilder.UseSqlite("Data Source=C:\\dev\\Examensarbete\\Database\\WebApiDatabaseTest.db", 
                b => b.MigrationsAssembly("Database"));

            return new WebApiDbContext(optionsBuilder.Options);
        }
    }
}


