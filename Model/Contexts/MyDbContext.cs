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

        public virtual DbSet<Building> Buildings { get; set; } = null!;
        public virtual DbSet<CustomerEmail> CustomerEmails { get; set; } = null!;
        public virtual DbSet<ImgName> ImgNames { get; set; } = null!;
        public virtual DbSet<Land> Lands { get; set; } = null!;
        public virtual DbSet<LandCustomer> LandCustomers { get; set; } = null!;
        public virtual DbSet<LandImg> LandImgs { get; set; } = null!;
        public virtual DbSet<MediaType> MediaTypes { get; set; } = null!;
        public virtual DbSet<Metro> Metros { get; set; } = null!;
        public virtual DbSet<Obyekt> Obyekts { get; set; } = null!;
        public virtual DbSet<ObyektImg> ObyektImgs { get; set; } = null!;
        public virtual DbSet<ObyektSecondStepCustomer> ObyektSecondStepCustomers { get; set; } = null!;
        public virtual DbSet<Office> Offices { get; set; } = null!;
        public virtual DbSet<OfficeCustomer> OfficeCustomers { get; set; } = null!;
        public virtual DbSet<OfficeImg> OfficeImgs { get; set; } = null!;
        public virtual DbSet<Region> Regions { get; set; } = null!;
        public virtual DbSet<RentHome> RentHomes { get; set; } = null!;
        public virtual DbSet<Room> Rooms { get; set; } = null!;
        public virtual DbSet<SecondStepCustomer> SecondStepCustomers { get; set; } = null!;
        public virtual DbSet<Sell> Sells { get; set; } = null!;
        public virtual DbSet<SellImg> SellImgs { get; set; } = null!;
        public virtual DbSet<SellSecondStepCustomer> SellSecondStepCustomers { get; set; } = null!;
        public virtual DbSet<UserModel> UserModels { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appSettings.json", optional: true, reloadOnChange: true)
                .Build();

                string ConnectionString = configuration["Password:DbContextLocal"];
                optionsBuilder.UseSqlServer(ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Building>(entity =>
            {
                entity.ToTable("Building");

                entity.Property(e => e.BuildingKind).HasMaxLength(50);

                entity.HasOne(d => d.BuildingForeign)
                    .WithMany(p => p.Buildings)
                    .HasForeignKey(d => d.BuildingForeignId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Building__Buildi__1D7B6025");
            });

            modelBuilder.Entity<CustomerEmail>(entity =>
            {
                entity.ToTable("CustomerEmail");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Fullname).HasMaxLength(50);

                entity.Property(e => e.Number).HasMaxLength(30);
            });

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

            modelBuilder.Entity<Land>(entity =>
            {
                entity.ToTable("Land");

                entity.Property(e => e.Addition).HasMaxLength(1000);

                entity.Property(e => e.Address).HasMaxLength(50);

                entity.Property(e => e.CoordinateX).HasMaxLength(50);

                entity.Property(e => e.CoordinateY).HasMaxLength(50);

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Document).HasMaxLength(50);

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Fullname).HasMaxLength(50);

                entity.Property(e => e.IsCalledWithCustomerFirstStep).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsCalledWithHomeOwnFirstStep).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsCalledWithHomeOwnThirdStep).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsPaidCustomerFirstStep).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsPaidHomeOwnFirstStep).HasDefaultValueSql("((0))");

                entity.Property(e => e.Looking).HasDefaultValueSql("((0))");

                entity.Property(e => e.Number).HasMaxLength(30);

                entity.Property(e => e.Recommend).HasDefaultValueSql("((0))");

                entity.Property(e => e.Region).HasMaxLength(50);
            });

            modelBuilder.Entity<LandCustomer>(entity =>
            {
                entity.HasKey(e => e.SecondStepCustomerId)
                    .HasName("PK__LandCust__735994EFD132E52A");

                entity.ToTable("LandCustomer");

                entity.Property(e => e.DirectCustomerDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FullName).HasMaxLength(50);

                entity.Property(e => e.Number).HasMaxLength(30);

                entity.HasOne(d => d.SecondStepCustomerForeign)
                    .WithMany(p => p.LandCustomers)
                    .HasForeignKey(d => d.SecondStepCustomerForeignId)
                    .HasConstraintName("FK__LandCusto__Secon__3CF40B7E");
            });

            modelBuilder.Entity<LandImg>(entity =>
            {
                entity.HasKey(e => e.ImgId)
                    .HasName("PK__LandImg__352F54F3A6AC1FE4");

                entity.ToTable("LandImg");

                entity.Property(e => e.ImgPath)
                    .HasMaxLength(500)
                    .HasColumnName("imgPath");

                entity.HasOne(d => d.ImgIdForeign)
                    .WithMany(p => p.LandImgs)
                    .HasForeignKey(d => d.ImgIdForeignId)
                    .HasConstraintName("FK__LandImg__ImgIdFo__3A179ED3");
            });

            modelBuilder.Entity<MediaType>(entity =>
            {
                entity.ToTable("MediaType");

                entity.Property(e => e.Counter)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Number)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Type)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Metro>(entity =>
            {
                entity.ToTable("Metro");

                entity.Property(e => e.MetroName).HasMaxLength(50);

                entity.HasOne(d => d.MetroForeign)
                    .WithMany(p => p.Metros)
                    .HasForeignKey(d => d.MetroForeignId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Metro__MetroFore__1A9EF37A");
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

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Fullname).HasMaxLength(50);

                entity.Property(e => e.IsCalledWithCustomerFirstStep).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsCalledWithHomeOwnFirstStep).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsCalledWithHomeOwnThirdStep).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsPaidCustomerFirstStep).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsPaidHomeOwnFirstStep).HasDefaultValueSql("((0))");

                entity.Property(e => e.Item).HasMaxLength(50);

                entity.Property(e => e.Looking).HasDefaultValueSql("((0))");

                entity.Property(e => e.Metro).HasMaxLength(50);

                entity.Property(e => e.Number).HasMaxLength(30);

                entity.Property(e => e.Recommend).HasDefaultValueSql("((0))");

                entity.Property(e => e.Region).HasMaxLength(50);

                entity.Property(e => e.Repair).HasMaxLength(50);

                entity.Property(e => e.SellOrRent).HasMaxLength(30);
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

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FullName).HasMaxLength(50);

                entity.Property(e => e.Number).HasMaxLength(30);

                entity.HasOne(d => d.SecondStepCustomerForeign)
                    .WithMany(p => p.ObyektSecondStepCustomers)
                    .HasForeignKey(d => d.SecondStepCustomerForeignId)
                    .HasConstraintName("FK__ObyektSec__Secon__5224328E");
            });

            modelBuilder.Entity<Office>(entity =>
            {
                entity.ToTable("office");

                entity.Property(e => e.Addition).HasMaxLength(1000);

                entity.Property(e => e.Address).HasMaxLength(50);

                entity.Property(e => e.CoordinateX).HasMaxLength(50);

                entity.Property(e => e.CoordinateY).HasMaxLength(50);

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Document).HasMaxLength(50);

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Fullname).HasMaxLength(50);

                entity.Property(e => e.IsCalledWithCustomerFirstStep).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsCalledWithHomeOwnFirstStep).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsCalledWithHomeOwnThirdStep).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsPaidCustomerFirstStep).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsPaidHomeOwnFirstStep).HasDefaultValueSql("((0))");

                entity.Property(e => e.Item).HasMaxLength(50);

                entity.Property(e => e.Looking).HasDefaultValueSql("((0))");

                entity.Property(e => e.Metro).HasMaxLength(50);

                entity.Property(e => e.Number).HasMaxLength(30);

                entity.Property(e => e.Recommend).HasDefaultValueSql("((0))");

                entity.Property(e => e.Region).HasMaxLength(50);

                entity.Property(e => e.Repair).HasMaxLength(50);

                entity.Property(e => e.SellOrRent).HasMaxLength(30);
            });

            modelBuilder.Entity<OfficeCustomer>(entity =>
            {
                entity.HasKey(e => e.SecondStepCustomerId)
                    .HasName("PK__OfficeCu__735994EFD7144C5C");

                entity.ToTable("OfficeCustomer");

                entity.Property(e => e.DirectCustomerDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FullName).HasMaxLength(50);

                entity.Property(e => e.Number).HasMaxLength(30);

                entity.HasOne(d => d.SecondStepCustomerForeign)
                    .WithMany(p => p.OfficeCustomers)
                    .HasForeignKey(d => d.SecondStepCustomerForeignId)
                    .HasConstraintName("FK__OfficeCus__Secon__5006DFF2");
            });

            modelBuilder.Entity<OfficeImg>(entity =>
            {
                entity.HasKey(e => e.ImgId)
                    .HasName("PK__OfficeIm__352F54F3F2F7CCDE");

                entity.ToTable("OfficeImg");

                entity.Property(e => e.ImgPath)
                    .HasMaxLength(500)
                    .HasColumnName("imgPath");

                entity.HasOne(d => d.ImgIdForeign)
                    .WithMany(p => p.OfficeImgs)
                    .HasForeignKey(d => d.ImgIdForeignId)
                    .HasConstraintName("FK__OfficeImg__ImgId__4D2A7347");
            });

            modelBuilder.Entity<Region>(entity =>
            {
                entity.ToTable("Region");

                entity.Property(e => e.RegionName).HasMaxLength(50);

                entity.HasOne(d => d.RegionForeign)
                    .WithMany(p => p.Regions)
                    .HasForeignKey(d => d.RegionForeignId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Region__RegionFo__2057CCD0");
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

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

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

                entity.Property(e => e.Item).HasMaxLength(50);

                entity.Property(e => e.Looking).HasDefaultValueSql("((0))");

                entity.Property(e => e.Metro).HasMaxLength(50);

                entity.Property(e => e.Number).HasMaxLength(30);

                entity.Property(e => e.Recommend).HasDefaultValueSql("((0))");

                entity.Property(e => e.Region).HasMaxLength(50);

                entity.Property(e => e.Repair).HasMaxLength(50);

                entity.Property(e => e.Sofa).HasDefaultValueSql("((0))");

                entity.Property(e => e.TableChair).HasDefaultValueSql("((0))");

                entity.Property(e => e.Tv).HasDefaultValueSql("((0))");

                entity.Property(e => e.Wardrobe).HasDefaultValueSql("((0))");

                entity.Property(e => e.WashingClothes).HasDefaultValueSql("((0))");

                entity.Property(e => e.Wifi).HasDefaultValueSql("((0))");

                entity.Property(e => e.WorkingBoy).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.ToTable("Room");

                entity.HasOne(d => d.RoomForeign)
                    .WithMany(p => p.Rooms)
                    .HasForeignKey(d => d.RoomForeignId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Room__RoomForeig__2334397B");
            });

            modelBuilder.Entity<SecondStepCustomer>(entity =>
            {
                entity.ToTable("SecondStepCustomer");

                entity.Property(e => e.DirectCustomerDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

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

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Floor).HasMaxLength(50);

                entity.Property(e => e.Fullname).HasMaxLength(50);

                entity.Property(e => e.IsCalledWithCustomerFirstStep).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsCalledWithHomeOwnFirstStep).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsCalledWithHomeOwnThirdStep).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsPaidCustomerFirstStep).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsPaidHomeOwnFirstStep).HasDefaultValueSql("((0))");

                entity.Property(e => e.Item).HasMaxLength(50);

                entity.Property(e => e.Looking).HasDefaultValueSql("((0))");

                entity.Property(e => e.Metro).HasMaxLength(50);

                entity.Property(e => e.Number).HasMaxLength(30);

                entity.Property(e => e.Recommend).HasDefaultValueSql("((0))");

                entity.Property(e => e.Region).HasMaxLength(50);

                entity.Property(e => e.Repair).HasMaxLength(50);

                entity.Property(e => e.VideoPath)
                    .HasMaxLength(200)
                    .IsUnicode(false);
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

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FullName).HasMaxLength(50);

                entity.Property(e => e.Number).HasMaxLength(30);

                entity.HasOne(d => d.SecondStepCustomerForeign)
                    .WithMany(p => p.SellSecondStepCustomers)
                    .HasForeignKey(d => d.SecondStepCustomerForeignId)
                    .HasConstraintName("FK__SEllSecon__Secon__6754599E");
            });

            modelBuilder.Entity<UserModel>(entity =>
            {
                entity.ToTable("UserModel");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RefreshToken)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TokenCreated).HasColumnType("datetime");

                entity.Property(e => e.TokenExpires).HasColumnType("datetime");

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
