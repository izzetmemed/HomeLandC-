using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
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
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=HomeLand;Integrated Security=True;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ImgName>(entity =>
            {
                entity.HasKey(e => e.ImgId)
                    .HasName("PK__ImgName__352F54F34756F032");

                entity.ToTable("ImgName");

                entity.Property(e => e.ImgPath)
                    .HasMaxLength(500)
                    .HasColumnName("imgPath");

                entity.HasOne(d => d.ImgIdForeign)
                    .WithMany(p => p.ImgNames)
                    .HasForeignKey(d => d.ImgIdForeignId)
                    .HasConstraintName("FK__ImgName__ImgIdFo__3D2915A8");
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

                entity.Property(e => e.Document).HasMaxLength(50);

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
                    .HasName("PK__obyektIm__352F54F3279336F7");

                entity.ToTable("obyektImg");

                entity.Property(e => e.ImgPath)
                    .HasMaxLength(500)
                    .HasColumnName("imgPath");

                entity.HasOne(d => d.ImgIdForeign)
                    .WithMany(p => p.ObyektImgs)
                    .HasForeignKey(d => d.ImgIdForeignId)
                    .HasConstraintName("FK__obyektImg__ImgId__4F47C5E3");
            });

            modelBuilder.Entity<ObyektSecondStepCustomer>(entity =>
            {
                entity.HasKey(e => e.SecondStepCustomerId)
                    .HasName("PK__ObyektSe__735994EFC2056489");

                entity.ToTable("ObyektSecondStepCustomer");

                entity.Property(e => e.DirectCustomerDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FullName).HasMaxLength(50);

                entity.Property(e => e.Number).HasMaxLength(30);

                entity.HasOne(d => d.SecondStepCustomerForeign)
                    .WithMany(p => p.ObyektSecondStepCustomers)
                    .HasForeignKey(d => d.SecondStepCustomerForeignId)
                    .HasConstraintName("FK__ObyektSec__Secon__5224328E");
            });

            modelBuilder.Entity<RentHome>(entity =>
            {
                entity.ToTable("RentHome");

                entity.Property(e => e.Addition).HasMaxLength(1000);

                entity.Property(e => e.Address).HasMaxLength(50);

                entity.Property(e => e.AirConditioning).HasDefaultValueSql("((0))");

                entity.Property(e => e.Bed).HasDefaultValueSql("((0))");

                entity.Property(e => e.Boy).HasDefaultValueSql("((0))");

                entity.Property(e => e.Building).HasMaxLength(50);

                entity.Property(e => e.CentralHeating).HasDefaultValueSql("((0))");

                entity.Property(e => e.Combi).HasDefaultValueSql("((0))");

                entity.Property(e => e.CoordinateX).HasMaxLength(50);

                entity.Property(e => e.CoordinateY).HasMaxLength(50);

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Family).HasDefaultValueSql("((0))");

                entity.Property(e => e.Floor).HasMaxLength(50);

                entity.Property(e => e.Fullname).HasMaxLength(50);

                entity.Property(e => e.GasHeating).HasDefaultValueSql("((0))");

                entity.Property(e => e.Girl).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsCalledWithCustomerFirstStep).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsCalledWithHomeOwnFirstStep).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsCalledWithHomeOwnThirdStep).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsPaidCustomerFirstStep).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsPaidHomeOwnFirstStep).HasDefaultValueSql("((0))");

                entity.Property(e => e.Metro).HasMaxLength(50);

                entity.Property(e => e.Number).HasMaxLength(30);

                entity.Property(e => e.Region).HasMaxLength(50);

                entity.Property(e => e.Repair).HasMaxLength(50);

                entity.Property(e => e.Sofa).HasDefaultValueSql("((0))");

                entity.Property(e => e.TableChair).HasDefaultValueSql("((0))");

                entity.Property(e => e.Tv).HasDefaultValueSql("((0))");

                entity.Property(e => e.Wardrobe).HasDefaultValueSql("((0))");

                entity.Property(e => e.WashingClothes).HasDefaultValueSql("((0))");

                entity.Property(e => e.Wifi).HasDefaultValueSql("((0))");

                entity.Property(e => e.WorkingBoy).HasDefaultValueSql("((0))");

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
                    .HasConstraintName("FK__SecondSte__Secon__40058253");
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
