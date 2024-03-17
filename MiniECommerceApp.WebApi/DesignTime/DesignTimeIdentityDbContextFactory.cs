using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using MiniECommerceApp.Data.Concrete;

namespace MiniECommerceApp.WebApi.DesignTime
{
    public class DesignTimeIdentityDbContextFactory : IDesignTimeDbContextFactory<MiniECommerceContext>
    {
        public MiniECommerceContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
            var builder = new DbContextOptionsBuilder<MiniECommerceContext>();
            var connectionstring = configuration.GetConnectionString("defaultConnection");
            ServerVersion serverVersion = ServerVersion.AutoDetect(connectionstring);
            builder.UseMySql(serverVersion, b => b.MigrationsAssembly("MiniECommerceApp.WebApi"));
            return new MiniECommerceContext(builder.Options);
        }
    }
}
