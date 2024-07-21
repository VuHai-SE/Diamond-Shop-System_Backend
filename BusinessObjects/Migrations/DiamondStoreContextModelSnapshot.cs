﻿// <auto-generated />
using System;
using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BusinessObjects.Migrations
{
    [DbContext(typeof(DiamondStoreContext))]
    partial class DiamondStoreContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BusinessObjects.TblAccount", b =>
                {
                    b.Property<int>("AccountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("AccountID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AccountId"));

                    b.Property<string>("Password")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Role")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Username")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("AccountId")
                        .HasName("PK__Tbl_Acco__349DA586C99D51EC");

                    b.HasIndex(new[] { "Username" }, "UQ__Tbl_Acco__536C85E43F82C222")
                        .IsUnique()
                        .HasFilter("[Username] IS NOT NULL");

                    b.ToTable("Tbl_Account", (string)null);
                });

            modelBuilder.Entity("BusinessObjects.TblCustomer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("CustomerID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CustomerId"));

                    b.Property<int?>("AccountId")
                        .HasColumnType("int")
                        .HasColumnName("AccountID");

                    b.Property<string>("Address")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime?>("Birthday")
                        .HasColumnType("datetime");

                    b.Property<double?>("DiscountRate")
                        .HasColumnType("float");

                    b.Property<string>("Email")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("FirstName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool?>("Gender")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Ranking")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<decimal>("Spending")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<bool?>("Status")
                        .HasColumnType("bit");

                    b.HasKey("CustomerId")
                        .HasName("PK__Tbl_Cust__A4AE64B85A5BC2D0");

                    b.HasIndex(new[] { "AccountId" }, "UQ__Tbl_Cust__349DA58780491564")
                        .IsUnique()
                        .HasFilter("[AccountID] IS NOT NULL");

                    b.ToTable("Tbl_Customer", (string)null);
                });

            modelBuilder.Entity("BusinessObjects.TblDiamondGradingReport", b =>
                {
                    b.Property<int>("ReportId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ReportID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReportId"));

                    b.Property<string>("GemId")
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)")
                        .HasColumnName("GemID");

                    b.Property<DateTime?>("GenerateDate")
                        .HasColumnType("datetime");

                    b.Property<string>("Image")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("ReportId")
                        .HasName("PK__Tbl_Diam__D5BD48E546D83403");

                    b.HasIndex(new[] { "GemId" }, "UQ__Tbl_Diam__F101D5A116B098A6")
                        .IsUnique()
                        .HasFilter("[GemID] IS NOT NULL");

                    b.ToTable("Tbl_DiamondGradingReport", (string)null);
                });

            modelBuilder.Entity("BusinessObjects.TblGem", b =>
                {
                    b.Property<string>("GemId")
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)")
                        .HasColumnName("GemID");

                    b.Property<double?>("CaratWeight")
                        .HasColumnType("float");

                    b.Property<string>("Clarity")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Color")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Cut")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Fluorescence")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("GemName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool?>("Origin")
                        .HasColumnType("bit");

                    b.Property<string>("Polish")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Shape")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Symmetry")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("GemId")
                        .HasName("PK__Tbl_Gem__F101D5A086487555");

                    b.ToTable("Tbl_Gem", (string)null);
                });

            modelBuilder.Entity("BusinessObjects.TblGemPriceList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double?>("CaratWeight")
                        .HasColumnType("float");

                    b.Property<string>("Clarity")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Color")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Cut")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("EffDate")
                        .HasColumnType("datetime");

                    b.Property<bool?>("Origin")
                        .HasColumnType("bit");

                    b.Property<double?>("Price")
                        .HasColumnType("float");

                    b.Property<double?>("Size")
                        .HasColumnType("float");

                    b.HasKey("Id")
                        .HasName("PK__Tbl_GemP__3214EC27195EBC95");

                    b.ToTable("Tbl_GemPriceList", (string)null);
                });

            modelBuilder.Entity("BusinessObjects.TblMaterialCategory", b =>
                {
                    b.Property<string>("MaterialId")
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)")
                        .HasColumnName("MaterialID");

                    b.Property<string>("MaterialName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("MaterialId")
                        .HasName("PK__Tbl_Mate__C50613171F3E1C81");

                    b.ToTable("Tbl_MaterialCategory", (string)null);
                });

            modelBuilder.Entity("BusinessObjects.TblMaterialPriceList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("EffDate")
                        .HasColumnType("datetime");

                    b.Property<string>("MaterialId")
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)")
                        .HasColumnName("MaterialID");

                    b.Property<double?>("UnitPrice")
                        .HasColumnType("float");

                    b.HasKey("Id")
                        .HasName("PK__Tbl_Mate__3214EC272938EEB3");

                    b.HasIndex(new[] { "MaterialId" }, "UQ__Tbl_Mate__C5061316CF12F515")
                        .IsUnique()
                        .HasFilter("[MaterialID] IS NOT NULL");

                    b.ToTable("Tbl_MaterialPriceList", (string)null);
                });

            modelBuilder.Entity("BusinessObjects.TblMembership", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double?>("DiscountRate")
                        .HasColumnType("float");

                    b.Property<double?>("MaxSpend")
                        .HasColumnType("float");

                    b.Property<double?>("MinSpend")
                        .HasColumnType("float");

                    b.Property<string>("Ranking")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Tbl_Membership", (string)null);
                });

            modelBuilder.Entity("BusinessObjects.TblOrder", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("OrderID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderId"));

                    b.Property<int?>("CustomerId")
                        .HasColumnType("int")
                        .HasColumnName("CustomerID");

                    b.Property<DateTime?>("OrderDate")
                        .HasColumnType("datetime");

                    b.Property<string>("OrderNote")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("OrderStatus")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PaymentMethod")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("ReceiveDate")
                        .HasColumnType("datetime");

                    b.Property<string>("ShipperId")
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)")
                        .HasColumnName("ShipperID");

                    b.Property<DateTime?>("ShippingDate")
                        .HasColumnType("datetime");

                    b.Property<string>("StaffId")
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)")
                        .HasColumnName("StaffID");

                    b.HasKey("OrderId")
                        .HasName("PK__Tbl_Orde__C3905BAF45C74DFD");

                    b.HasIndex("CustomerId");

                    b.HasIndex("ShipperId");

                    b.HasIndex("StaffId");

                    b.ToTable("Tbl_Order", (string)null);
                });

            modelBuilder.Entity("BusinessObjects.TblOrderDetail", b =>
                {
                    b.Property<int>("OrderDetailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("OrderDetailID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderDetailId"));

                    b.Property<double?>("CustomizedAmount")
                        .HasColumnType("float");

                    b.Property<int?>("CustomizedSize")
                        .HasColumnType("int");

                    b.Property<double?>("FinalPrice")
                        .HasColumnType("float");

                    b.Property<int?>("OrderId")
                        .HasColumnType("int")
                        .HasColumnName("OrderID");

                    b.Property<string>("ProductId")
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)")
                        .HasColumnName("ProductID");

                    b.Property<int?>("Quantity")
                        .HasColumnType("int");

                    b.Property<double?>("TotalPrice")
                        .HasColumnType("float");

                    b.HasKey("OrderDetailId")
                        .HasName("PK__Tbl_Orde__D3B9D30CBDF411D5");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("Tbl_OrderDetail", (string)null);
                });

            modelBuilder.Entity("BusinessObjects.TblPayment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal?>("Amount")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<string>("Currency")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<int?>("CustomerId")
                        .HasColumnType("int")
                        .HasColumnName("CustomerID");

                    b.Property<double?>("Deposits")
                        .HasColumnType("float");

                    b.Property<int?>("OrderId")
                        .HasColumnType("int")
                        .HasColumnName("OrderID");

                    b.Property<string>("PayDetail")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("PayerEmail")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("PaymentDate")
                        .HasColumnType("datetime");

                    b.Property<string>("PaymentMethod")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PaymentStatus")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("TransactionId")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("TransactionID");

                    b.HasKey("Id")
                        .HasName("PK__Tbl_Paym__3214EC278AF50C7D");

                    b.HasIndex("CustomerId");

                    b.HasIndex(new[] { "OrderId" }, "UQ__Tbl_Paym__C3905BAEE2026040")
                        .IsUnique()
                        .HasFilter("[OrderID] IS NOT NULL");

                    b.ToTable("Tbl_Payment", (string)null);
                });

            modelBuilder.Entity("BusinessObjects.TblProduct", b =>
                {
                    b.Property<string>("ProductId")
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)")
                        .HasColumnName("ProductID");

                    b.Property<string>("CategoryId")
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)")
                        .HasColumnName("CategoryID");

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<double?>("GemCost")
                        .HasColumnType("float");

                    b.Property<int?>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("Image")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<double?>("MaterialCost")
                        .HasColumnType("float");

                    b.Property<double?>("PriceRate")
                        .HasColumnType("float");

                    b.Property<string>("ProductCode")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ProductName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("ProductSize")
                        .HasColumnType("int");

                    b.Property<double?>("ProductionCost")
                        .HasColumnType("float");

                    b.Property<bool?>("Status")
                        .HasColumnType("bit");

                    b.Property<double?>("UnitSizePrice")
                        .HasColumnType("float");

                    b.HasKey("ProductId")
                        .HasName("PK__Tbl_Prod__B40CC6EDE0D42E6F");

                    b.HasIndex("CategoryId");

                    b.ToTable("Tbl_Product", (string)null);
                });

            modelBuilder.Entity("BusinessObjects.TblProductCategory", b =>
                {
                    b.Property<string>("CategoryId")
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)")
                        .HasColumnName("CategoryID");

                    b.Property<string>("CategoryName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("CategoryId")
                        .HasName("PK__Tbl_Prod__19093A2BE43A65B1");

                    b.ToTable("Tbl_ProductCategory", (string)null);
                });

            modelBuilder.Entity("BusinessObjects.TblProductGem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("GemId")
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)")
                        .HasColumnName("GemID");

                    b.Property<string>("ProductId")
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)")
                        .HasColumnName("ProductID");

                    b.HasKey("Id")
                        .HasName("PK__Tbl_Prod__3214EC2763B1E6B2");

                    b.HasIndex(new[] { "ProductId" }, "UQ__Tbl_Prod__B40CC6ECC4F2941B")
                        .IsUnique()
                        .HasFilter("[ProductID] IS NOT NULL");

                    b.HasIndex(new[] { "GemId" }, "UQ__Tbl_Prod__F101D5A1E13D6D16")
                        .IsUnique()
                        .HasFilter("[GemID] IS NOT NULL");

                    b.ToTable("Tbl_ProductGem", (string)null);
                });

            modelBuilder.Entity("BusinessObjects.TblProductMaterial", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("MaterialId")
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)")
                        .HasColumnName("MaterialID");

                    b.Property<string>("ProductId")
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)")
                        .HasColumnName("ProductID");

                    b.Property<double?>("Weight")
                        .HasColumnType("float");

                    b.HasKey("Id")
                        .HasName("PK__Tbl_Prod__3214EC271DB74E5F");

                    b.HasIndex("MaterialId");

                    b.HasIndex(new[] { "ProductId" }, "UQ__Tbl_Prod__B40CC6EC57E47CFD")
                        .IsUnique()
                        .HasFilter("[ProductID] IS NOT NULL");

                    b.ToTable("Tbl_ProductMaterial", (string)null);
                });

            modelBuilder.Entity("BusinessObjects.TblRefund", b =>
                {
                    b.Property<int>("RefundId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("RefundID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RefundId"));

                    b.Property<int?>("PaymentId")
                        .HasColumnType("int")
                        .HasColumnName("PaymentID");

                    b.Property<string>("Reason")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("RefundAmount")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<DateTime?>("RefundDate")
                        .HasColumnType("datetime");

                    b.Property<string>("RefundStatus")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("RefundId")
                        .HasName("PK__Tbl_Refu__725AB90042AC7B45");

                    b.HasIndex(new[] { "PaymentId" }, "UQ__Tbl_Refu__9B556A5944646271")
                        .IsUnique()
                        .HasFilter("[PaymentID] IS NOT NULL");

                    b.ToTable("Tbl_Refund", (string)null);
                });

            modelBuilder.Entity("BusinessObjects.TblSaleStaff", b =>
                {
                    b.Property<string>("StaffId")
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)")
                        .HasColumnName("StaffID");

                    b.Property<int?>("AccountId")
                        .HasColumnType("int")
                        .HasColumnName("AccountID");

                    b.Property<string>("FirstName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("LastName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("StaffId")
                        .HasName("PK__Tbl_Sale__96D4AAF7B4C34DBD");

                    b.HasIndex(new[] { "AccountId" }, "UQ__Tbl_Sale__349DA587D74B32BF")
                        .IsUnique()
                        .HasFilter("[AccountID] IS NOT NULL");

                    b.ToTable("Tbl_SaleStaff", (string)null);
                });

            modelBuilder.Entity("BusinessObjects.TblShipper", b =>
                {
                    b.Property<string>("ShipperId")
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)")
                        .HasColumnName("ShipperID");

                    b.Property<int?>("AccountId")
                        .HasColumnType("int")
                        .HasColumnName("AccountID");

                    b.Property<string>("FirstName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("LastName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("ShipperId")
                        .HasName("PK__Tbl_Ship__1F8AFFB94914BF77");

                    b.HasIndex(new[] { "AccountId" }, "UQ__Tbl_Ship__349DA587CB2371CC")
                        .IsUnique()
                        .HasFilter("[AccountID] IS NOT NULL");

                    b.ToTable("Tbl_Shipper", (string)null);
                });

            modelBuilder.Entity("BusinessObjects.TblWarranty", b =>
                {
                    b.Property<int>("WarrantyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("WarrantyID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("WarrantyId"));

                    b.Property<int?>("OrderDetailId")
                        .HasColumnType("int")
                        .HasColumnName("OrderDetailID");

                    b.Property<DateTime?>("WarrantyEndDate")
                        .HasColumnType("datetime");

                    b.Property<byte[]>("WarrantyPdf")
                        .HasColumnType("varbinary(max)");

                    b.Property<DateTime?>("WarrantyStartDate")
                        .HasColumnType("datetime");

                    b.HasKey("WarrantyId")
                        .HasName("PK__Tbl_Warr__2ED318F3EBA9260C");

                    b.HasIndex(new[] { "OrderDetailId" }, "UQ__Tbl_Warr__D3B9D30DB1A48541")
                        .IsUnique()
                        .HasFilter("[OrderDetailID] IS NOT NULL");

                    b.ToTable("Tbl_Warranty", (string)null);
                });

            modelBuilder.Entity("BusinessObjects.TblCustomer", b =>
                {
                    b.HasOne("BusinessObjects.TblAccount", "Account")
                        .WithOne("TblCustomer")
                        .HasForeignKey("BusinessObjects.TblCustomer", "AccountId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("FK__Tbl_Custo__Accou__3C69FB99");

                    b.Navigation("Account");
                });

            modelBuilder.Entity("BusinessObjects.TblDiamondGradingReport", b =>
                {
                    b.HasOne("BusinessObjects.TblGem", "Gem")
                        .WithOne("TblDiamondGradingReport")
                        .HasForeignKey("BusinessObjects.TblDiamondGradingReport", "GemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK__Tbl_Diamo__GemID__5FB337D6");

                    b.Navigation("Gem");
                });

            modelBuilder.Entity("BusinessObjects.TblMaterialPriceList", b =>
                {
                    b.HasOne("BusinessObjects.TblMaterialCategory", "Material")
                        .WithOne("TblMaterialPriceList")
                        .HasForeignKey("BusinessObjects.TblMaterialPriceList", "MaterialId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK__Tbl_Mater__Mater__276EDEB3");

                    b.Navigation("Material");
                });

            modelBuilder.Entity("BusinessObjects.TblOrder", b =>
                {
                    b.HasOne("BusinessObjects.TblCustomer", "Customer")
                        .WithMany("TblOrders")
                        .HasForeignKey("CustomerId")
                        .HasConstraintName("FK__Tbl_Order__Custo__4E88ABD4");

                    b.HasOne("BusinessObjects.TblShipper", "Shipper")
                        .WithMany("TblOrders")
                        .HasForeignKey("ShipperId")
                        .HasConstraintName("FK__Tbl_Order__Shipp__5070F446");

                    b.HasOne("BusinessObjects.TblSaleStaff", "Staff")
                        .WithMany("TblOrders")
                        .HasForeignKey("StaffId")
                        .HasConstraintName("FK__Tbl_Order__Staff__4F7CD00D");

                    b.Navigation("Customer");

                    b.Navigation("Shipper");

                    b.Navigation("Staff");
                });

            modelBuilder.Entity("BusinessObjects.TblOrderDetail", b =>
                {
                    b.HasOne("BusinessObjects.TblOrder", "Order")
                        .WithMany("TblOrderDetails")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("FK__Tbl_Order__Order__534D60F1");

                    b.HasOne("BusinessObjects.TblProduct", "Product")
                        .WithMany("TblOrderDetails")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK__Tbl_Order__Produ__5441852A");

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("BusinessObjects.TblPayment", b =>
                {
                    b.HasOne("BusinessObjects.TblCustomer", "Customer")
                        .WithMany("TblPayments")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("FK__Tbl_Payme__Custo__17F790F9");

                    b.HasOne("BusinessObjects.TblOrder", "Order")
                        .WithOne("TblPayment")
                        .HasForeignKey("BusinessObjects.TblPayment", "OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK__Tbl_Payme__Order__17036CC0");

                    b.Navigation("Customer");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("BusinessObjects.TblProduct", b =>
                {
                    b.HasOne("BusinessObjects.TblProductCategory", "Category")
                        .WithMany("TblProducts")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK__Tbl_Produ__Categ__412EB0B6");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("BusinessObjects.TblProductGem", b =>
                {
                    b.HasOne("BusinessObjects.TblGem", "Gem")
                        .WithOne("TblProductGem")
                        .HasForeignKey("BusinessObjects.TblProductGem", "GemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK__Tbl_Produ__GemID__4BAC3F29");

                    b.HasOne("BusinessObjects.TblProduct", "Product")
                        .WithOne("TblProductGem")
                        .HasForeignKey("BusinessObjects.TblProductGem", "ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK__Tbl_Produ__Produ__4AB81AF0");

                    b.Navigation("Gem");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("BusinessObjects.TblProductMaterial", b =>
                {
                    b.HasOne("BusinessObjects.TblMaterialCategory", "Material")
                        .WithMany("TblProductMaterials")
                        .HasForeignKey("MaterialId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK__Tbl_Produ__Mater__45F365D3");

                    b.HasOne("BusinessObjects.TblProduct", "Product")
                        .WithOne("TblProductMaterial")
                        .HasForeignKey("BusinessObjects.TblProductMaterial", "ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK__Tbl_Produ__Produ__44FF419A");

                    b.Navigation("Material");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("BusinessObjects.TblRefund", b =>
                {
                    b.HasOne("BusinessObjects.TblPayment", "Payment")
                        .WithOne("TblRefund")
                        .HasForeignKey("BusinessObjects.TblRefund", "PaymentId")
                        .HasConstraintName("FK__Tbl_Refun__Payme__1BC821DD");

                    b.Navigation("Payment");
                });

            modelBuilder.Entity("BusinessObjects.TblSaleStaff", b =>
                {
                    b.HasOne("BusinessObjects.TblAccount", "Account")
                        .WithOne("TblSaleStaff")
                        .HasForeignKey("BusinessObjects.TblSaleStaff", "AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK__Tbl_SaleS__Accou__33D4B598");

                    b.Navigation("Account");
                });

            modelBuilder.Entity("BusinessObjects.TblShipper", b =>
                {
                    b.HasOne("BusinessObjects.TblAccount", "Account")
                        .WithOne("TblShipper")
                        .HasForeignKey("BusinessObjects.TblShipper", "AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK__Tbl_Shipp__Accou__37A5467C");

                    b.Navigation("Account");
                });

            modelBuilder.Entity("BusinessObjects.TblWarranty", b =>
                {
                    b.HasOne("BusinessObjects.TblOrderDetail", "OrderDetail")
                        .WithOne("TblWarranty")
                        .HasForeignKey("BusinessObjects.TblWarranty", "OrderDetailId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK__Tbl_Warra__Order__5BE2A6F2");

                    b.Navigation("OrderDetail");
                });

            modelBuilder.Entity("BusinessObjects.TblAccount", b =>
                {
                    b.Navigation("TblCustomer");

                    b.Navigation("TblSaleStaff");

                    b.Navigation("TblShipper");
                });

            modelBuilder.Entity("BusinessObjects.TblCustomer", b =>
                {
                    b.Navigation("TblOrders");

                    b.Navigation("TblPayments");
                });

            modelBuilder.Entity("BusinessObjects.TblGem", b =>
                {
                    b.Navigation("TblDiamondGradingReport");

                    b.Navigation("TblProductGem");
                });

            modelBuilder.Entity("BusinessObjects.TblMaterialCategory", b =>
                {
                    b.Navigation("TblMaterialPriceList");

                    b.Navigation("TblProductMaterials");
                });

            modelBuilder.Entity("BusinessObjects.TblOrder", b =>
                {
                    b.Navigation("TblOrderDetails");

                    b.Navigation("TblPayment");
                });

            modelBuilder.Entity("BusinessObjects.TblOrderDetail", b =>
                {
                    b.Navigation("TblWarranty");
                });

            modelBuilder.Entity("BusinessObjects.TblPayment", b =>
                {
                    b.Navigation("TblRefund");
                });

            modelBuilder.Entity("BusinessObjects.TblProduct", b =>
                {
                    b.Navigation("TblOrderDetails");

                    b.Navigation("TblProductGem");

                    b.Navigation("TblProductMaterial");
                });

            modelBuilder.Entity("BusinessObjects.TblProductCategory", b =>
                {
                    b.Navigation("TblProducts");
                });

            modelBuilder.Entity("BusinessObjects.TblSaleStaff", b =>
                {
                    b.Navigation("TblOrders");
                });

            modelBuilder.Entity("BusinessObjects.TblShipper", b =>
                {
                    b.Navigation("TblOrders");
                });
#pragma warning restore 612, 618
        }
    }
}
