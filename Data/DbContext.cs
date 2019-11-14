using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Data.Entities;

namespace Data
{
    public class DbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbContext(DbContextOptions<DbContext> options) : base(options)
        {

        }
        public DbSet<User> Users {get; set;}
        public DbSet<Dashboard> Dashboards { get; set; }

    }
}
