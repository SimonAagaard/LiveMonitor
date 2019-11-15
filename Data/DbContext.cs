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
        //Used to get the configuration string from API, apparently the program runs just fine without it
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

    //DbContext for identity 
    public class DbContext : IdentityDbContext<MonitorUser>
    {
        public DbContext(DbContextOptions<DbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Dashboard>()
            //    .HasOne(typeof(DashboardSetting))
            //    .WithOne()
            //    .HasForeignKey("DashboardSettingId");

            modelBuilder.Entity<Dashboard>()
                .HasOne(x => x.MonitorUserFK)
                .WithMany(d => d.Dashboards)
                .HasForeignKey("UserId");

            modelBuilder.Entity<DashboardSetting>()
                .HasOne(x => x.DashboardIdFK)
                .WithOne(y => y.DashboardSettingFK)
                .HasForeignKey<DashboardSetting>(y => y.DashboardId);
        }

        public DbSet<MonitorUser> MonitorUsers { get; set;}
        public DbSet<Dashboard> Dashboards { get; set; }
        public DbSet<DashboardSetting> DashboardSettings { get; set; }
        public DbSet<DashboardType> DashboardTypes { get; set; }
        public DbSet<DataSet> Datasets { get; set; }
        public DbSet<Integration> Integrations { get; set; }
        public DbSet<IntegrationSetting> IntegrationSettings { get; set; }
    }
}