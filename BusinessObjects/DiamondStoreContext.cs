using System;
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

    public virtual DbSet<TblProductGem> TblProductGems { get; set; }

    public virtual DbSet<TblProductMaterial> TblProductMaterials { get; set; }

    public virtual DbSet<TblSaleStaff> TblSaleStaffs { get; set; }

    public virtual DbSet<TblShipper> TblShippers { get; set; }

    public virtual DbSet<TblWarranty> TblWarranties { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;uid=sa;pwd=12345;database=DiamondStore;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblAccount>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PK__Tbl_Acco__349DA5867A3CB22A");

            entity.ToTable("Tbl_Account");

            entity.HasIndex(e => e.Username, "UQ__Tbl_Acco__536C85E4899DDF6C").IsUnique();

            entity.Property(e => e.AccountId).HasColumnName("AccountID");
            entity.Property(e => e.Password).HasMaxLength(100);
            entity.Property(e => e.Role).HasMaxLength(50);
            entity.Property(e => e.Username).HasMaxLength(100);
        });

        modelBuilder.Entity<TblCustomer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Tbl_Cust__A4AE64B8D4574B2B");

            entity.ToTable("Tbl_Customer");

            entity.HasIndex(e => e.AccountId, "UQ__Tbl_Cust__349DA58781A0120F").IsUnique();

            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.AccountId).HasColumnName("AccountID");
            entity.Property(e => e.Address).HasMaxLength(200);
            entity.Property(e => e.Birthday).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.Gender).HasMaxLength(10);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.PhoneNumber).HasMaxLength(10);
            entity.Property(e => e.Ranking).HasMaxLength(10);

            entity.HasOne(d => d.Account).WithOne(p => p.TblCustomer)
                .HasForeignKey<TblCustomer>(d => d.AccountId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Tbl_Custo__Accou__3B75D760");
        });

        modelBuilder.Entity<TblDiamondGradingReport>(entity =>
        {
            entity.HasKey(e => e.ReportId).HasName("PK__Tbl_Diam__D5BD48E559EBAA91");

            entity.ToTable("Tbl_DiamondGradingReport");

            entity.HasIndex(e => e.GemId, "UQ__Tbl_Diam__F101D5A16B2514C0").IsUnique();

            entity.Property(e => e.ReportId).HasColumnName("ReportID");
            entity.Property(e => e.GemId)
                .HasMaxLength(8)
                .HasColumnName("GemID");
            entity.Property(e => e.GenerateDate).HasColumnType("datetime");
            entity.Property(e => e.Image).HasMaxLength(255);

            entity.HasOne(d => d.Gem).WithOne(p => p.TblDiamondGradingReport)
                .HasForeignKey<TblDiamondGradingReport>(d => d.GemId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Tbl_Diamo__GemID__5AEE82B9");
        });

        modelBuilder.Entity<TblGem>(entity =>
        {
            entity.HasKey(e => e.GemId).HasName("PK__Tbl_Gem__F101D5A07B278F9C");

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
            entity.HasKey(e => e.Id).HasName("PK__Tbl_GemP__3214EC27470D111A");

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
            entity.HasKey(e => e.MaterialId).HasName("PK__Tbl_Mate__C5061317FB365EFA");

            entity.ToTable("Tbl_MaterialCategory");

            entity.Property(e => e.MaterialId)
                .HasMaxLength(8)
                .HasColumnName("MaterialID");
            entity.Property(e => e.MaterialName).HasMaxLength(100);
        });

        modelBuilder.Entity<TblMaterialPriceList>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tbl_Mate__3214EC2776484C8F");

            entity.ToTable("Tbl_MaterialPriceList");

            entity.HasIndex(e => e.MaterialId, "UQ__Tbl_Mate__C5061316C58730F9").IsUnique();

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
            entity.HasKey(e => e.OrderId).HasName("PK__Tbl_Orde__C3905BAF083B2DB2");

            entity.ToTable("Tbl_Order");

            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.OrderDate).HasColumnType("datetime");
            entity.Property(e => e.OrderStatus).HasMaxLength(50);
            entity.Property(e => e.PaymentMethod).HasMaxLength(50);
            entity.Property(e => e.ReceiveDate).HasColumnType("datetime");
            entity.Property(e => e.ShipStatus).HasMaxLength(50);
            entity.Property(e => e.ShipperId)
                .HasMaxLength(8)
                .HasColumnName("ShipperID");
            entity.Property(e => e.ShippingDate).HasColumnType("datetime");
            entity.Property(e => e.StaffId)
                .HasMaxLength(8)
                .HasColumnName("StaffID");

            entity.HasOne(d => d.Customer).WithMany(p => p.TblOrders)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__Tbl_Order__Custo__49C3F6B7");

            entity.HasOne(d => d.Shipper).WithMany(p => p.TblOrders)
                .HasForeignKey(d => d.ShipperId)
                .HasConstraintName("FK__Tbl_Order__Shipp__4BAC3F29");

            entity.HasOne(d => d.Staff).WithMany(p => p.TblOrders)
                .HasForeignKey(d => d.StaffId)
                .HasConstraintName("FK__Tbl_Order__Staff__4AB81AF0");
        });

        modelBuilder.Entity<TblOrderDetail>(entity =>
        {
            entity.HasKey(e => e.OrderDetailId).HasName("PK__Tbl_Orde__D3B9D30C4F6CA3BC");

            entity.ToTable("Tbl_OrderDetail");

            entity.Property(e => e.OrderDetailId).HasColumnName("OrderDetailID");
            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.ProductId)
                .HasMaxLength(8)
                .HasColumnName("ProductID");

            entity.HasOne(d => d.Order).WithMany(p => p.TblOrderDetails)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Tbl_Order__Order__4E88ABD4");

            entity.HasOne(d => d.Product).WithMany(p => p.TblOrderDetails)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Tbl_Order__Produ__4F7CD00D");
        });

        modelBuilder.Entity<TblPayment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tbl_Paym__3214EC276FBDE05C");

            entity.ToTable("Tbl_Payment");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.PayDetail).HasMaxLength(255);
            entity.Property(e => e.PaymentMethod).HasMaxLength(50);

            entity.HasOne(d => d.Customer).WithMany(p => p.TblPayments)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Tbl_Payme__Custo__534D60F1");

            entity.HasOne(d => d.Order).WithMany(p => p.TblPayments)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Tbl_Payme__Order__52593CB8");
        });

        modelBuilder.Entity<TblProduct>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Tbl_Prod__B40CC6ED10614BBF");

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
                .HasConstraintName("FK__Tbl_Produ__Categ__3F466844");
        });

        modelBuilder.Entity<TblProductCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Tbl_Prod__19093A2B30F8D990");

            entity.ToTable("Tbl_ProductCategory");

            entity.Property(e => e.CategoryId)
                .HasMaxLength(8)
                .HasColumnName("CategoryID");
            entity.Property(e => e.CategoryName).HasMaxLength(100);
        });

        modelBuilder.Entity<TblProductGem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tbl_Prod__3214EC275E5C27AB");

            entity.ToTable("Tbl_ProductGem");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.GemId)
                .HasMaxLength(8)
                .HasColumnName("GemID");
            entity.Property(e => e.ProductId)
                .HasMaxLength(8)
                .HasColumnName("ProductID");

            entity.HasOne(d => d.Gem).WithMany(p => p.TblProductGems)
                .HasForeignKey(d => d.GemId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Tbl_Produ__GemID__46E78A0C");

            entity.HasOne(d => d.Product).WithMany(p => p.TblProductGems)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Tbl_Produ__Produ__45F365D3");
        });

        modelBuilder.Entity<TblProductMaterial>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tbl_Prod__3214EC27CAEDCFDB");

            entity.ToTable("Tbl_ProductMaterial");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.MaterialId)
                .HasMaxLength(8)
                .HasColumnName("MaterialID");
            entity.Property(e => e.ProductId)
                .HasMaxLength(8)
                .HasColumnName("ProductID");

            entity.HasOne(d => d.Material).WithMany(p => p.TblProductMaterials)
                .HasForeignKey(d => d.MaterialId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Tbl_Produ__Mater__4316F928");

            entity.HasOne(d => d.Product).WithMany(p => p.TblProductMaterials)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Tbl_Produ__Produ__4222D4EF");
        });

        modelBuilder.Entity<TblSaleStaff>(entity =>
        {
            entity.HasKey(e => e.StaffId).HasName("PK__Tbl_Sale__96D4AAF7753BDD16");

            entity.ToTable("Tbl_SaleStaff");

            entity.HasIndex(e => e.AccountId, "UQ__Tbl_Sale__349DA587C3FA4ED5").IsUnique();

            entity.Property(e => e.StaffId)
                .HasMaxLength(8)
                .HasColumnName("StaffID");
            entity.Property(e => e.AccountId).HasColumnName("AccountID");
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);

            entity.HasOne(d => d.Account).WithOne(p => p.TblSaleStaff)
                .HasForeignKey<TblSaleStaff>(d => d.AccountId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Tbl_SaleS__Accou__33D4B598");
        });

        modelBuilder.Entity<TblShipper>(entity =>
        {
            entity.HasKey(e => e.ShipperId).HasName("PK__Tbl_Ship__1F8AFFB98E0FC68D");

            entity.ToTable("Tbl_Shipper");

            entity.HasIndex(e => e.AccountId, "UQ__Tbl_Ship__349DA5871E3B5E50").IsUnique();

            entity.Property(e => e.ShipperId)
                .HasMaxLength(8)
                .HasColumnName("ShipperID");
            entity.Property(e => e.AccountId).HasColumnName("AccountID");
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);

            entity.HasOne(d => d.Account).WithOne(p => p.TblShipper)
                .HasForeignKey<TblShipper>(d => d.AccountId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Tbl_Shipp__Accou__37A5467C");
        });

        modelBuilder.Entity<TblWarranty>(entity =>
        {
            entity.HasKey(e => e.WarrantyId).HasName("PK__Tbl_Warr__2ED318F39E89CE68");

            entity.ToTable("Tbl_Warranty");

            entity.HasIndex(e => e.OrderDetailId, "UQ__Tbl_Warr__D3B9D30DAED36541").IsUnique();

            entity.Property(e => e.WarrantyId).HasColumnName("WarrantyID");
            entity.Property(e => e.OrderDetailId).HasColumnName("OrderDetailID");
            entity.Property(e => e.WarrantyEndDate).HasColumnType("datetime");
            entity.Property(e => e.WarrantyStartDate).HasColumnType("datetime");

            entity.HasOne(d => d.OrderDetail).WithOne(p => p.TblWarranty)
                .HasForeignKey<TblWarranty>(d => d.OrderDetailId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Tbl_Warra__Order__571DF1D5");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
