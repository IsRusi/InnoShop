using InnoShop.UserManager.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace UserManager.Infrastructure.Data
{
    public class UserContextFactory: IDesignTimeDbContextFactory<UserContext>
    {

        public  UserContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true)
                .Build();

            string connectionString = configuration.GetConnectionString("DefaultConnection")!;
            string provider = configuration["DatabaseProvider"]!;

            DbContextOptionsBuilder<UserContext> dbContextOptionsBuilder = new DbContextOptionsBuilder<UserContext>();

            switch (provider)
            {
                case "PostgreSQL": dbContextOptionsBuilder.UseNpgsql(connectionString); break;
                default: throw new Exception($"Unknown database provider: {provider}");
            }

            return new UserContext(dbContextOptionsBuilder.Options);
        }
    }
}
