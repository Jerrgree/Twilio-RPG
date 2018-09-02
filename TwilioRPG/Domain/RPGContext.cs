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
        public virtual DbSet<Log> Logs { get; set; }

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

                entity.HasMany(e => e.Passwords)
                    .WithOne(e => e.User)
                    .HasForeignKey(x => x.User_Id)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_User_Passwords");

                entity.Property(e => e.CreateDateUtc);
            });
            
            modelBuilder.Entity<Password>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.PasswordText)
                    .IsRequired(true);

                entity.Property(e => e.User_Id)
                    .IsRequired(true);

                entity.Property(e => e.IsActive)
                    .IsRequired(true)
                    .HasDefaultValue(true);
            });

            modelBuilder.Entity<Log>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.DateUtc)
                    .IsRequired();

                entity.Property(e => e.LogLevel)
                    .IsRequired(true)
                    .HasMaxLength(20);

                entity.Property(e => e.Source)
                    .IsRequired(true)
                    .HasMaxLength(20);

                entity.Property(e => e.Destination)
                    .IsRequired(true)
                    .HasMaxLength(20);

                entity.Property(e => e.Controller)
                    .HasMaxLength(100);

                entity.Property(e => e.Method)
                    .HasMaxLength(100);

                entity.Property(e => e.Message)
                    .IsRequired(true);

                entity.Property(e => e.Exception);

                entity.Property(e => e.Conversation)
                    .IsRequired(true)
                    .HasMaxLength(200);
            });
        }
    }
}
