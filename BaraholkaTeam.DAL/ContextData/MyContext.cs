using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaraholkaTeam.DAL.ContextData
{
    public class MyContext : DbContext
    {
        public DbSet<User> users { get; set; }
        public DbSet<Product> products { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Server=91.238.103.51;Port=5743;Database=denysdb;Username=denys;Password=qwerty1*;");
        }
    }
}
