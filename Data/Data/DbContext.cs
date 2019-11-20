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
                .AddJsonFile(@Directory.GetCurrentDirectory() + "/../Web/appsettings.json")
                .Build();
            var builder = new DbContextOptionsBuilder<DbContext>();
            var connectionString = configuration.GetConnectionString("LiveMonitorChristoffer");
            builder.UseSqlServer(connectionString);
            return new DbContext(builder.Options);
        }
    }

    //DbContext for identity 
    public class DbContext : IdentityDbContext<MonitorUser, MonitorRole, Guid>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(@Directory.GetCurrentDirectory() + "/../Web/appsettings.json")
                .Build();

                string conn = configuration.GetConnectionString("LiveMonitorConnection");

                builder.UseSqlServer(conn);
            }
            base.OnConfiguring(builder);
        }


        public DbContext(DbContextOptions<DbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MonitorUser>(x =>
            {
                x.Property(y => y.Id).HasDefaultValueSql("newsequentialid()");
            });

            modelBuilder.Entity<MonitorRole>(x =>
            {
                x.Property(y => y.Id).HasDefaultValueSql("newsequentialid()");
            });

            modelBuilder.Entity<Dashboard>()
                .HasOne(x => x.MonitorUser)
                .WithMany(d => d.Dashboards)
                .HasForeignKey("UserId");

            modelBuilder.Entity<DashboardSetting>()
                .HasOne(x => x.Dashboard)
                .WithOne(y => y.DashboardSetting)
                .HasForeignKey<DashboardSetting>(y => y.DashboardId);

            modelBuilder.Entity<Integration>()
                .HasOne(x => x.MonitorUser)
                .WithMany(x => x.Integrations)
                .HasForeignKey("UserId");

            modelBuilder.Entity<IntegrationSetting>()
                .HasOne(x => x.Integration)
                .WithOne(y => y.IntegrationSetting)
                .HasForeignKey<IntegrationSetting>(y => y.IntegrationId);

            modelBuilder.Entity<DataSet>()
                .HasOne(x => x.IntegrationSetting)
                .WithMany(y => y.DataSets)
                .HasForeignKey("IntegrationSettingId");
        }

        public DbSet<MonitorUser> MonitorUsers { get; set;}
        public DbSet<MonitorRole> MonitorRoles { get; set; }
        public DbSet<Dashboard> Dashboards { get; set; }
        public DbSet<DashboardSetting> DashboardSettings { get; set; }
        public DbSet<DashboardType> DashboardTypes { get; set; }
        public DbSet<DataSet> Datasets { get; set; }
        public DbSet<Integration> Integrations { get; set; }
        public DbSet<IntegrationSetting> IntegrationSettings { get; set; }
    }
}