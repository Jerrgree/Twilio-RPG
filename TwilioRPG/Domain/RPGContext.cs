using System;
using System.Collections;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Domain.Models;

namespace Domain
{
    public class RPGContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // TODO: Move connection to config
                optionsBuilder.UseSqlServer(@"Server=localhost;Database=TwinoidRPGDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasAlternateKey(e => e.Number);

                entity.Property(e => e.Number)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .IsRequired(true);

                entity.Property(e => e.IsActive)
                    .IsRequired(true)
                    .HasDefaultValue(true);

                entity.Property(e => e.CreateDateUtc);
            });
        }
    }
}
