using Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace API;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<WebApiDbContext>
{
    public WebApiDbContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .AddUserSecrets<Program>()
            .Build();

        var builder = new DbContextOptionsBuilder<WebApiDbContext>();

        var connectionString = configuration.GetConnectionString("azure");

        builder.UseSqlServer(connectionString);

        return new WebApiDbContext(builder.Options);
    }
}