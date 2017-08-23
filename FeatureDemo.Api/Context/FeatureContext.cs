using System;
using FeatureDemo.Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using FeatureDemo.Api.Utilities;

namespace FeatureDemo.Api.Context
{
    public class FeatureContext : DbContext
    {
        public DbSet<Item> Items { get; set; }
        public DbSet<Atm> Atms { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Institution> Institutions { get; set; }

        public FeatureContext(DbContextOptions<FeatureContext> contextOptions) : base(contextOptions){}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connStr = AppSettings.FeatureDatabase;
            if(!string.IsNullOrEmpty(connStr) && !optionsBuilder.IsConfigured)
                optionsBuilder.UseMySql(connStr);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Feature");

            modelBuilder.Entity<Atm>()
                        .HasKey(k => k.Id);
            modelBuilder.Entity<Atm>()
                        .HasOne(k => k.Institution)
                        .WithMany(m => m.Atms)
                        .HasForeignKey(f => f.InstitutionId);

            modelBuilder.Entity<User>()
                        .HasKey(k => k.Id);
            modelBuilder.Entity<User>()
                        .HasOne(o => o.Institution)
                        .WithMany(m => m.Users)
                        .HasForeignKey(f => f.InstitutionId);
            modelBuilder.Entity<User>()
                        .Property(p => p.FirstName)
                        .HasMaxLength(50);
			modelBuilder.Entity<User>()
						.Property(p => p.LastName)
						.HasMaxLength(50);
			modelBuilder.Entity<User>()
                        .Property(p => p.Email)
						.HasMaxLength(50);
			modelBuilder.Entity<User>()
                        .Property(p => p.PhoneNumber)
						.HasMaxLength(20);

            modelBuilder.Entity<Institution>()
                        .HasKey(k => k.Id);
            modelBuilder.Entity<Institution>()
                        .HasMany(m => m.Atms)
                        .WithOne(o => o.Institution);
            modelBuilder.Entity<Institution>()
                        .Property(p => p.Name)
                        .HasMaxLength(256);

            modelBuilder.Entity<Item>()
                        .HasKey(k => k.Id);
            modelBuilder.Entity<Item>().ToTable("Item");
            
        }
    }
}
