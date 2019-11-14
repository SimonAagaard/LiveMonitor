using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Data.Entities;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<DbContext>
    {
        public DbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(@Directory.GetCurrentDirectory() + "/../API/appsettings.json")
                .Build();
            var builder = new DbContextOptionsBuilder<DbContext>();
            var connectionString = configuration.GetConnectionString("LiveMonitorConnection");
            builder.UseSqlServer(connectionString);
            return new DbContext(builder.Options);
        }

    }

    public class DbContext : IdentityDbContext<MonitorUser>
    {
        public DbContext(DbContextOptions<DbContext> options) : base(options)
        {

        }
        public DbSet<MonitorUser> Users { get; set;}
        //public DbSet<Dashboard> Dashboards { get; set; }

    }
}
