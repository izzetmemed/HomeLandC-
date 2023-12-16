using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Model.Models;
namespace Model.Contexts
{
    public partial class MyDbContext : DbContext
    {
        public MyDbContext()
        {
        }

        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ImgName> ImgNames { get; set; } = null!;
        public virtual DbSet<Obyekt> Obyekts { get; set; } = null!;
        public virtual DbSet<ObyektImg> ObyektImgs { get; set; } = null!;
        public virtual DbSet<ObyektSecondStepCustomer> ObyektSecondStepCustomers { get; set; } = null!;
        public virtual DbSet<RentHome> RentHomes { get; set; } = null!;
        public virtual DbSet<SecondStepCustomer> SecondStepCustomers { get; set; } = null!;
        public virtual DbSet<Sell> Sells { get; set; } = null!;      
        public virtual DbSet<SellImg> SellImgs { get; set; } = null!;
        public virtual DbSet<SellSecondStepCustomer> SellSecondStepCustomers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appSettings.json", optional: true, reloadOnChange: true)
            .Build();


                optionsBuilder.UseSqlServer(configuration["Password:DbContext"]);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ImgName>(entity =>
            {
                entity.HasKey(e => e.ImgId)
                    .HasName("PK__ImgName__352F54F31F4E6D60");

                entity.ToTable("ImgName");

                entity.Property(e => e.ImgPath)
                    .HasMaxLength(500)
                    .HasColumnName("imgPath");

                entity.HasOne(d => d.ImgIdForeign)
                    .WithMany(p => p.ImgNames)
                    .HasForeignKey(d => d.ImgIdForeignId)
                    .HasConstraintName("FK__ImgName__ImgIdFo__534D60F1");
            });

            modelBuilder.Entity<Obyekt>(entity =>
            {
                entity.ToTable("Obyekt");

                entity.Property(e => e.Addition).HasMaxLength(1000);

                entity.Property(e => e.Address).HasMaxLength(50);

                entity.Property(e => e.CoordinateX).HasMaxLength(50);

                entity.Property(e => e.CoordinateY).HasMaxLength(50);

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Fullname).HasMaxLength(50);

                entity.Property(e => e.IsCalledWithCustomerFirstStep).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsCalledWithHomeOwnFirstStep).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsCalledWithHomeOwnThirdStep).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsPaidCustomerFirstStep).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsPaidHomeOwnFirstStep).HasDefaultValueSql("((0))");

                entity.Property(e => e.Metro).HasMaxLength(50);

                entity.Property(e => e.Number).HasMaxLength(30);

                entity.Property(e => e.Region).HasMaxLength(50);

                entity.Property(e => e.Repair).HasMaxLength(50);

                entity.Property(e => e.SellOrRent).HasMaxLength(30);

                entity.Property(e => e.İtem).HasMaxLength(50);
            });

            modelBuilder.Entity<ObyektImg>(entity =>
            {
                entity.HasKey(e => e.ImgId)
                    .HasName("PK__obyektIm__352F54F3E27873D2");

                entity.ToTable("obyektImg");

                entity.Property(e => e.ImgPath)
                    .HasMaxLength(500)
                    .HasColumnName("imgPath");

                entity.HasOne(d => d.ImgIdForeign)
                    .WithMany(p => p.ObyektImgs)
                    .HasForeignKey(d => d.ImgIdForeignId)
                    .HasConstraintName("FK__obyektImg__ImgId__75A278F5");
            });

            modelBuilder.Entity<ObyektSecondStepCustomer>(entity =>
            {
                entity.HasKey(e => e.SecondStepCustomerId)
                    .HasName("PK__ObyektSe__735994EFA9966745");

                entity.ToTable("ObyektSecondStepCustomer");

                entity.Property(e => e.DirectCustomerDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FullName).HasMaxLength(50);

                entity.Property(e => e.Number).HasMaxLength(30);

                entity.HasOne(d => d.SecondStepCustomerForeign)
                    .WithMany(p => p.ObyektSecondStepCustomers)
                    .HasForeignKey(d => d.SecondStepCustomerForeignId)
                    .HasConstraintName("FK__ObyektSec__Secon__787EE5A0");
            });

            modelBuilder.Entity<RentHome>(entity =>
            {
                entity.ToTable("RentHome");

                entity.Property(e => e.Addition).HasMaxLength(1000);

                entity.Property(e => e.Address).HasMaxLength(50);

                entity.Property(e => e.Building).HasMaxLength(50);

                entity.Property(e => e.CoordinateX).HasMaxLength(50);

                entity.Property(e => e.CoordinateY).HasMaxLength(50);

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Floor).HasMaxLength(50);

                entity.Property(e => e.Fullname).HasMaxLength(50);

                entity.Property(e => e.IsCalledWithCustomerFirstStep).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsCalledWithHomeOwnFirstStep).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsCalledWithHomeOwnThirdStep).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsPaidCustomerFirstStep).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsPaidHomeOwnFirstStep).HasDefaultValueSql("((0))");

                entity.Property(e => e.Metro).HasMaxLength(50);

                entity.Property(e => e.Number).HasMaxLength(30);

                entity.Property(e => e.Region).HasMaxLength(50);

                entity.Property(e => e.Repair).HasMaxLength(50);

                entity.Property(e => e.İtem).HasMaxLength(50);
            });

            modelBuilder.Entity<SecondStepCustomer>(entity =>
            {
                entity.ToTable("SecondStepCustomer");

                entity.Property(e => e.DirectCustomerDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FullName).HasMaxLength(50);

                entity.Property(e => e.Number).HasMaxLength(30);

                entity.HasOne(d => d.SecondStepCustomerForeign)
                    .WithMany(p => p.SecondStepCustomers)
                    .HasForeignKey(d => d.SecondStepCustomerForeignId)
                    .HasConstraintName("FK__SecondSte__Secon__5629CD9C");
            });

            modelBuilder.Entity<Sell>(entity =>
            {
                entity.ToTable("Sell");

                entity.Property(e => e.Addition).HasMaxLength(1000);

                entity.Property(e => e.Address).HasMaxLength(50);

                entity.Property(e => e.Building).HasMaxLength(50);

                entity.Property(e => e.CoordinateX).HasMaxLength(50);

                entity.Property(e => e.CoordinateY).HasMaxLength(50);

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Document).HasMaxLength(50);

                entity.Property(e => e.Floor).HasMaxLength(50);

                entity.Property(e => e.Fullname).HasMaxLength(50);

                entity.Property(e => e.IsCalledWithCustomerFirstStep).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsCalledWithHomeOwnFirstStep).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsCalledWithHomeOwnThirdStep).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsPaidCustomerFirstStep).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsPaidHomeOwnFirstStep).HasDefaultValueSql("((0))");

                entity.Property(e => e.Metro).HasMaxLength(50);

                entity.Property(e => e.Number).HasMaxLength(30);

                entity.Property(e => e.Region).HasMaxLength(50);

                entity.Property(e => e.Repair).HasMaxLength(50);

                entity.Property(e => e.İtem).HasMaxLength(50);
            });

            modelBuilder.Entity<SellImg>(entity =>
            {
                entity.HasKey(e => e.ImgId)
                    .HasName("PK__SellImg__352F54F3681E4ABC");

                entity.ToTable("SellImg");

                entity.Property(e => e.ImgPath)
                    .HasMaxLength(500)
                    .HasColumnName("imgPath");

                entity.HasOne(d => d.ImgIdForeign)
                    .WithMany(p => p.SellImgs)
                    .HasForeignKey(d => d.ImgIdForeignId)
                    .HasConstraintName("FK__SellImg__ImgIdFo__6477ECF3");
            });

            modelBuilder.Entity<SellSecondStepCustomer>(entity =>
            {
                entity.HasKey(e => e.SecondStepCustomerId)
                    .HasName("PK__SEllSeco__735994EFE0350DBF");

                entity.ToTable("SEllSecondStepCustomer");

                entity.Property(e => e.DirectCustomerDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FullName).HasMaxLength(50);

                entity.Property(e => e.Number).HasMaxLength(30);

                entity.HasOne(d => d.SecondStepCustomerForeign)
                    .WithMany(p => p.SellSecondStepCustomers)
                    .HasForeignKey(d => d.SecondStepCustomerForeignId)
                    .HasConstraintName("FK__SEllSecon__Secon__6754599E");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
