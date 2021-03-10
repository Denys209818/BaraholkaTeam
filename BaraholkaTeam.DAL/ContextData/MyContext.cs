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
        public DbSet<FilterName> FilterNames { get; set; }
        public DbSet<FilterValue> FilterValues { get; set; }
        public DbSet<FilterNameValue> FilterNameValues { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Server=91.238.103.51;Port=5743;Database=denysdb;Username=denys;Password=qwerty1*;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FilterNameValue>(nameValue => {
                nameValue.HasKey(primaryKeys => new { primaryKeys.FilterNameId, primaryKeys.FilterValueId });

                nameValue.HasOne(virtualElementFromFilterName => virtualElementFromFilterName.FilterName)
                .WithMany(virtualCollectionFromFilterNameValue => virtualCollectionFromFilterNameValue.NameValues)
                .HasForeignKey(intElementWithForeignKeyParam => intElementWithForeignKeyParam.FilterNameId)
                .IsRequired();

                nameValue.HasOne(virtualElementFromFilterValueForEntityToNameValue =>
                virtualElementFromFilterValueForEntityToNameValue.FilterValue)
                .WithMany(virtualCollectionWithEntityToFilterValue => virtualCollectionWithEntityToFilterValue.NameValues)
                .HasForeignKey(intParamWithForeignKeySettings => intParamWithForeignKeySettings.FilterValueId)
                .IsRequired();
            });

           
        }
    }
}
