using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace NorthwindShopCore
{
    public partial class CompaniesRangeContext : DbContext
    {
        public CompaniesRangeContext()
        {
        }

        public CompaniesRangeContext(DbContextOptions<CompaniesRangeContext> options)
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
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=CompaniesRange;Username=postgres;Password=1111");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>(entity =>
            {
                entity.ToTable("company");

                entity.Property(e => e.Companyid)
                    .HasColumnName("companyid")
                    .HasDefaultValueSql("nextval('company_sequence'::regclass)");

                entity.Property(e => e.Investorid).HasColumnName("investorid");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name");

                entity.Property(e => e.Ownerid).HasColumnName("ownerid");

                entity.Property(e => e.Profitperyear).HasColumnName("profitperyear");

                entity.Property(e => e.Typeid).HasColumnName("typeid");

                entity.HasOne(d => d.Investor)
                    .WithMany(p => p.Company)
                    .HasForeignKey(d => d.Investorid)
                    .HasConstraintName("investor_foreign");

                entity.HasOne(d => d.Owner)
                    .WithMany(p => p.Company)
                    .HasForeignKey(d => d.Ownerid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("owner_foreign");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Company)
                    .HasForeignKey(d => d.Typeid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("category_foreign");
            });

            modelBuilder.Entity<Companytype>(entity =>
            {
                entity.ToTable("companytype");

                entity.Property(e => e.Companytypeid)
                    .HasColumnName("companytypeid")
                    .HasDefaultValueSql("nextval('companytype_sequence'::regclass)");

                entity.Property(e => e.Nametype).HasColumnName("nametype");
            });

            modelBuilder.Entity<Investor>(entity =>
            {
                entity.ToTable("investor");

                entity.Property(e => e.Investorid)
                    .HasColumnName("investorid")
                    .HasDefaultValueSql("nextval('investor_sequence'::regclass)");

                entity.Property(e => e.Capital).HasColumnName("capital");

                entity.Property(e => e.Companyid).HasColumnName("companyid");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Owner>(entity =>
            {
                entity.ToTable("owner");

                entity.Property(e => e.Ownerid)
                    .HasColumnName("ownerid")
                    .HasDefaultValueSql("nextval('general_sequence'::regclass)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name");

                entity.Property(e => e.Salary).HasColumnName("salary");
            });

            modelBuilder.HasSequence("company_sequence");

            modelBuilder.HasSequence("companytype_sequence").StartsAt(2);

            modelBuilder.HasSequence("general_sequence");

            modelBuilder.HasSequence("investor_sequence");
        }
    }
}
