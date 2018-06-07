using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace NorthwindShopCore
{
    public partial class CompaniesRangeJSONContext : DbContext
    {
        public CompaniesRangeJSONContext()
        {
        }

        public CompaniesRangeJSONContext(DbContextOptions<CompaniesRangeJSONContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<Companytype> Companytype { get; set; }
        public virtual DbSet<Investor> Investor { get; set; }
        public virtual DbSet<Owner> Owner { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=CompaniesRangeJSON;Username=postgres;Password=1111");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>(entity =>
            {
                entity.ToTable("company");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Info)
                    .IsRequired()
                    .HasColumnName("info")
                    .HasColumnType("json");
            });

            modelBuilder.Entity<Companytype>(entity =>
            {
                entity.ToTable("companytype");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Info)
                    .IsRequired()
                    .HasColumnName("info")
                    .HasColumnType("json");
            });

            modelBuilder.Entity<Investor>(entity =>
            {
                entity.ToTable("investor");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Info)
                    .IsRequired()
                    .HasColumnName("info")
                    .HasColumnType("json");
            });

            modelBuilder.Entity<Owner>(entity =>
            {
                entity.ToTable("owner");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Info)
                    .IsRequired()
                    .HasColumnName("info")
                    .HasColumnType("json");
            });
        }
    }
}
