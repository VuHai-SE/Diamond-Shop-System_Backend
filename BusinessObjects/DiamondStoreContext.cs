﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BusinessObjects;

public partial class DiamondStoreContext : DbContext
{
    public DiamondStoreContext()
    {
    }

    public DiamondStoreContext(DbContextOptions<DiamondStoreContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblAccount> TblAccounts { get; set; }

    public virtual DbSet<TblCustomer> TblCustomers { get; set; }

    public virtual DbSet<TblDiamondGradingReport> TblDiamondGradingReports { get; set; }

    public virtual DbSet<TblGem> TblGems { get; set; }

    public virtual DbSet<TblGemPriceList> TblGemPriceLists { get; set; }

    public virtual DbSet<TblMaterialCategory> TblMaterialCategories { get; set; }

    public virtual DbSet<TblMaterialPriceList> TblMaterialPriceLists { get; set; }

    public virtual DbSet<TblMembership> TblMemberships { get; set; }

    public virtual DbSet<TblOrder> TblOrders { get; set; }

    public virtual DbSet<TblOrderDetail> TblOrderDetails { get; set; }

    public virtual DbSet<TblPayment> TblPayments { get; set; }

    public virtual DbSet<TblProduct> TblProducts { get; set; }

    public virtual DbSet<TblProductCategory> TblProductCategories { get; set; }

    public virtual DbSet<TblProductMaterial> TblProductMaterials { get; set; }

    public virtual DbSet<TblStaff> TblStaffs { get; set; }

    public virtual DbSet<TblWarranty> TblWarranties { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;uid=sa;pwd=12345;database=DiamondStore;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblAccount>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PK__Tbl_Acco__349DA5867ADF35A8");

            entity.ToTable("Tbl_Account");

            entity.HasIndex(e => e.Username, "UQ__Tbl_Acco__536C85E44347055E").IsUnique();

            entity.Property(e => e.AccountId)
                .HasMaxLength(8)
                .HasColumnName("AccountID");
            entity.Property(e => e.Password).HasMaxLength(100);
            entity.Property(e => e.Role).HasMaxLength(50);
            entity.Property(e => e.Username).HasMaxLength(100);
        });

        modelBuilder.Entity<TblCustomer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Tbl_Cust__A4AE64B8C8CB26DA");

            entity.ToTable("Tbl_Customer");

            entity.Property(e => e.CustomerId)
                .HasMaxLength(8)
                .HasColumnName("CustomerID");
            entity.Property(e => e.AccountId)
                .HasMaxLength(8)
                .HasColumnName("AccountID");
            entity.Property(e => e.Address).HasMaxLength(200);
            entity.Property(e => e.Birthday).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.Gender).HasMaxLength(10);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.PhoneNumber).HasMaxLength(10);
            entity.Property(e => e.Ranking).HasMaxLength(10);

            entity.HasOne(d => d.Account).WithMany(p => p.TblCustomers)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Tbl_Custo__Accou__36B12243");
        });

        modelBuilder.Entity<TblDiamondGradingReport>(entity =>
        {
            entity.HasKey(e => e.ReportId).HasName("PK__Tbl_Diam__D5BD48E5ACF3216E");

            entity.ToTable("Tbl_DiamondGradingReport");

            entity.HasIndex(e => e.GemId, "UQ__Tbl_Diam__F101D5A15BF1F2BE").IsUnique();

            entity.Property(e => e.ReportId)
                .HasMaxLength(8)
                .HasColumnName("ReportID");
            entity.Property(e => e.GemId)
                .HasMaxLength(8)
                .HasColumnName("GemID");
            entity.Property(e => e.GenerateDate).HasColumnType("datetime");
            entity.Property(e => e.Image).HasMaxLength(255);

            entity.HasOne(d => d.Gem).WithOne(p => p.TblDiamondGradingReport)
                .HasForeignKey<TblDiamondGradingReport>(d => d.GemId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Tbl_Diamo__GemID__5441852A");
        });

        modelBuilder.Entity<TblGem>(entity =>
        {
            entity.HasKey(e => e.GemId).HasName("PK__Tbl_Gem__F101D5A0CC383096");

            entity.ToTable("Tbl_Gem");

            entity.Property(e => e.GemId)
                .HasMaxLength(8)
                .HasColumnName("GemID");
            entity.Property(e => e.Clarity).HasMaxLength(50);
            entity.Property(e => e.Color).HasMaxLength(50);
            entity.Property(e => e.Cut).HasMaxLength(50);
            entity.Property(e => e.Fluorescence).HasMaxLength(50);
            entity.Property(e => e.GemName).HasMaxLength(50);
            entity.Property(e => e.Origin).HasMaxLength(100);
            entity.Property(e => e.Polish).HasMaxLength(50);
            entity.Property(e => e.Shape).HasMaxLength(50);
            entity.Property(e => e.Symmetry).HasMaxLength(50);
        });

        modelBuilder.Entity<TblGemPriceList>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tbl_GemP__3214EC2708DD23BE");

            entity.ToTable("Tbl_GemPriceList");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Clarity).HasMaxLength(50);
            entity.Property(e => e.Color).HasMaxLength(50);
            entity.Property(e => e.Cut).HasMaxLength(50);
            entity.Property(e => e.EffDate).HasColumnType("datetime");
            entity.Property(e => e.Origin).HasMaxLength(100);
        });

        modelBuilder.Entity<TblMaterialCategory>(entity =>
        {
            entity.HasKey(e => e.MaterialId).HasName("PK__Tbl_Mate__C50613171622D165");

            entity.ToTable("Tbl_MaterialCategory");

            entity.Property(e => e.MaterialId)
                .HasMaxLength(8)
                .HasColumnName("MaterialID");
            entity.Property(e => e.MaterialName).HasMaxLength(100);
        });

        modelBuilder.Entity<TblMaterialPriceList>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tbl_Mate__3214EC27B4831432");

            entity.ToTable("Tbl_MaterialPriceList");

            entity.HasIndex(e => e.MaterialId, "UQ__Tbl_Mate__C50613168298B295").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.EffDate).HasColumnType("datetime");
            entity.Property(e => e.MaterialId)
                .HasMaxLength(8)
                .HasColumnName("MaterialID");

            entity.HasOne(d => d.Material).WithOne(p => p.TblMaterialPriceList)
                .HasForeignKey<TblMaterialPriceList>(d => d.MaterialId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Tbl_Mater__Mater__276EDEB3");
        });

        modelBuilder.Entity<TblMembership>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Tbl_Membership");

            entity.Property(e => e.Ranking).HasMaxLength(50);
        });

        modelBuilder.Entity<TblOrder>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Tbl_Orde__C3905BAFA3C9AEB3");

            entity.ToTable("Tbl_Order");

            entity.Property(e => e.OrderId)
                .HasMaxLength(8)
                .HasColumnName("OrderID");
            entity.Property(e => e.CustomerId)
                .HasMaxLength(8)
                .HasColumnName("CustomerID");
            entity.Property(e => e.OrderDate).HasColumnType("datetime");
            entity.Property(e => e.OrderStatus).HasMaxLength(50);
            entity.Property(e => e.PaymentMethod).HasMaxLength(50);
            entity.Property(e => e.ReceiveDate).HasColumnType("datetime");
            entity.Property(e => e.ShipperId)
                .HasMaxLength(8)
                .HasColumnName("ShipperID");
            entity.Property(e => e.ShippingDate).HasColumnType("datetime");
            entity.Property(e => e.StaffId)
                .HasMaxLength(8)
                .HasColumnName("StaffID");

            entity.HasOne(d => d.Customer).WithMany(p => p.TblOrders)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Tbl_Order__Custo__44FF419A");

            entity.HasOne(d => d.Staff).WithMany(p => p.TblOrders)
                .HasForeignKey(d => d.StaffId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Tbl_Order__Staff__45F365D3");
        });

        modelBuilder.Entity<TblOrderDetail>(entity =>
        {
            entity.HasKey(e => e.OrderDetailId).HasName("PK__Tbl_Orde__D3B9D30C0DEBB22D");

            entity.ToTable("Tbl_OrderDetail");

            entity.Property(e => e.OrderDetailId)
                .HasMaxLength(8)
                .HasColumnName("OrderDetailID");
            entity.Property(e => e.OrderId)
                .HasMaxLength(8)
                .HasColumnName("OrderID");
            entity.Property(e => e.ProductId)
                .HasMaxLength(8)
                .HasColumnName("ProductID");

            entity.HasOne(d => d.Order).WithMany(p => p.TblOrderDetails)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Tbl_Order__Order__48CFD27E");

            entity.HasOne(d => d.Product).WithMany(p => p.TblOrderDetails)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Tbl_Order__Produ__49C3F6B7");
        });

        modelBuilder.Entity<TblPayment>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Tbl_Payment");

            entity.Property(e => e.CustomerId)
                .HasMaxLength(8)
                .HasColumnName("CustomerID");
            entity.Property(e => e.OrderId)
                .HasMaxLength(8)
                .HasColumnName("OrderID");
            entity.Property(e => e.PayDetail).HasMaxLength(255);
            entity.Property(e => e.PaymentMethod).HasMaxLength(50);

            entity.HasOne(d => d.Customer).WithMany()
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Tbl_Payme__Custo__4CA06362");

            entity.HasOne(d => d.Order).WithMany()
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Tbl_Payme__Order__4BAC3F29");
        });

        modelBuilder.Entity<TblProduct>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Tbl_Prod__B40CC6EDC2BE8400");

            entity.ToTable("Tbl_Product");

            entity.Property(e => e.ProductId)
                .HasMaxLength(8)
                .HasColumnName("ProductID");
            entity.Property(e => e.CategoryId)
                .HasMaxLength(8)
                .HasColumnName("CategoryID");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.Image).HasMaxLength(255);
            entity.Property(e => e.ProductCode).HasMaxLength(50);
            entity.Property(e => e.ProductName).HasMaxLength(100);

            entity.HasOne(d => d.Category).WithMany(p => p.TblProducts)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Tbl_Produ__Categ__3A81B327");

            entity.HasMany(d => d.Gems).WithMany(p => p.Products)
                .UsingEntity<Dictionary<string, object>>(
                    "TblProductGem",
                    r => r.HasOne<TblGem>().WithMany()
                        .HasForeignKey("GemId")
                        .HasConstraintName("FK__Tbl_Produ__GemID__4222D4EF"),
                    l => l.HasOne<TblProduct>().WithMany()
                        .HasForeignKey("ProductId")
                        .HasConstraintName("FK__Tbl_Produ__Produ__412EB0B6"),
                    j =>
                    {
                        j.HasKey("ProductId", "GemId").HasName("PK__Tbl_Prod__AB1CDBB7E43079B5");
                        j.ToTable("Tbl_ProductGem");
                        j.IndexerProperty<string>("ProductId")
                            .HasMaxLength(8)
                            .HasColumnName("ProductID");
                        j.IndexerProperty<string>("GemId")
                            .HasMaxLength(8)
                            .HasColumnName("GemID");
                    });
        });

        modelBuilder.Entity<TblProductCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Tbl_Prod__19093A2B0200BE5A");

            entity.ToTable("Tbl_ProductCategory");

            entity.Property(e => e.CategoryId)
                .HasMaxLength(8)
                .HasColumnName("CategoryID");
            entity.Property(e => e.CategoryName).HasMaxLength(100);
        });

        modelBuilder.Entity<TblProductMaterial>(entity =>
        {
            entity.HasKey(e => new { e.ProductId, e.MaterialId }).HasName("PK__Tbl_Prod__D85CA7DC593BB93E");

            entity.ToTable("Tbl_ProductMaterial");

            entity.Property(e => e.ProductId)
                .HasMaxLength(8)
                .HasColumnName("ProductID");
            entity.Property(e => e.MaterialId)
                .HasMaxLength(8)
                .HasColumnName("MaterialID");

            entity.HasOne(d => d.Material).WithMany(p => p.TblProductMaterials)
                .HasForeignKey(d => d.MaterialId)
                .HasConstraintName("FK__Tbl_Produ__Mater__3E52440B");

            entity.HasOne(d => d.Product).WithMany(p => p.TblProductMaterials)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__Tbl_Produ__Produ__3D5E1FD2");
        });

        modelBuilder.Entity<TblStaff>(entity =>
        {
            entity.HasKey(e => e.StaffId).HasName("PK__Tbl_Staf__96D4AAF7001894A6");

            entity.ToTable("Tbl_Staff");

            entity.HasIndex(e => e.AccountId, "UQ__Tbl_Staf__349DA5876E727721").IsUnique();

            entity.Property(e => e.StaffId)
                .HasMaxLength(8)
                .HasColumnName("StaffID");
            entity.Property(e => e.AccountId)
                .HasMaxLength(8)
                .HasColumnName("AccountID");
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);

            entity.HasOne(d => d.Account).WithOne(p => p.TblStaff)
                .HasForeignKey<TblStaff>(d => d.AccountId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Tbl_Staff__Accou__33D4B598");
        });

        modelBuilder.Entity<TblWarranty>(entity =>
        {
            entity.HasKey(e => e.WarrantyId).HasName("PK__Tbl_Warr__2ED318F3294E9FAC");

            entity.ToTable("Tbl_Warranty");

            entity.HasIndex(e => e.OrderDetailId, "UQ__Tbl_Warr__D3B9D30DC4EE2FAF").IsUnique();

            entity.Property(e => e.WarrantyId)
                .HasMaxLength(8)
                .HasColumnName("WarrantyID");
            entity.Property(e => e.OrderDetailId)
                .HasMaxLength(8)
                .HasColumnName("OrderDetailID");
            entity.Property(e => e.WarrantyEndDate).HasColumnType("datetime");
            entity.Property(e => e.WarrantyStartDate).HasColumnType("datetime");

            entity.HasOne(d => d.OrderDetail).WithOne(p => p.TblWarranty)
                .HasForeignKey<TblWarranty>(d => d.OrderDetailId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Tbl_Warra__Order__5070F446");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}