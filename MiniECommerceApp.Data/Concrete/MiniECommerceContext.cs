using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MiniECommerceApp.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerceApp.Data.Concrete
{
    public class MiniECommerceContext : IdentityDbContext<User>
    {
        public MiniECommerceContext(DbContextOptions<MiniECommerceContext> dbContextOptions) : base(dbContextOptions)
        {

        }
        public MiniECommerceContext()
        {
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connectionString = @"Server=localhost;Port=3306;Database=MiniECommerceAppDb;Uid=sysadmin;Pwd=45867-Sas";
                ServerVersion serverVersion = ServerVersion.AutoDetect(connectionString);
                optionsBuilder.UseMySql(connectionString, serverVersion);
            }
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()));
        }

    }
}
