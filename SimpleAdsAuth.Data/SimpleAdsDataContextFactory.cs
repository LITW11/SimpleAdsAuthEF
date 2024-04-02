using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace SimpleAdsAuth.Data
{
    public class SimpleAdsDataContextFactory : IDesignTimeDbContextFactory<SimpleAdsDataContext>
    {
        public SimpleAdsDataContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
               .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(),
               $"..{Path.DirectorySeparatorChar}SimpleAdsAuth.Web"))
               .AddJsonFile("appsettings.json")
               .AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true).Build();

            return new SimpleAdsDataContext(config.GetConnectionString("ConStr"));
        }
    }
}
