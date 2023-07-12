using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using VentionTest.Domain.Entities;

namespace VentionTest.DAL.DbContexts
{
    public class VentionTestDbContext : DbContext
    {
        public VentionTestDbContext(DbContextOptions<VentionTestDbContext> options)
            : base(options)
        {
        }
        public DbSet<Capital> Capital { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<Language> Language { get; set; }
        public DbSet<LanguageCountry> LanguageCountry { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>()
                .HasOne(c => c.Capital)
                .WithOne(c => c.Country)
                .HasForeignKey<Capital>(c => c.CountryId);

            modelBuilder.Entity<Country>()
                .HasMany(c => c.Cities)
                .WithOne(c => c.Country)
                .HasForeignKey(c => c.CountryId);

            modelBuilder.Entity<Country>()
                .HasMany(c => c.Languages)
                .WithMany(l => l.Countries)
                .UsingEntity<LanguageCountry>(
                    lc => lc
                    .HasOne(lc => lc.Language) 
                    .WithMany(l => l.LanguageCountries)
                    .HasForeignKey(lc => lc.LanguageId),
                    lc => lc
                    .HasOne(lc => lc.Country)
                    .WithMany(c => c.LanguageCountries)
                    .HasForeignKey(lc => lc.CountryId)
                );
        }
    }
}
