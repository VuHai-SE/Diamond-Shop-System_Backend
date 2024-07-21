using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObjects.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tbl_Account",
                columns: table => new
                {
                    AccountID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Role = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Tbl_Acco__349DA586C99D51EC", x => x.AccountID);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_Gem",
                columns: table => new
                {
                    GemID = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    GemName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Polish = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Symmetry = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Fluorescence = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Origin = table.Column<bool>(type: "bit", nullable: true),
                    CaratWeight = table.Column<double>(type: "float", nullable: true),
                    Color = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Cut = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Clarity = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Shape = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Tbl_Gem__F101D5A086487555", x => x.GemID);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_GemPriceList",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Origin = table.Column<bool>(type: "bit", nullable: true),
                    CaratWeight = table.Column<double>(type: "float", nullable: true),
                    Color = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Cut = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Clarity = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Price = table.Column<double>(type: "float", nullable: true),
                    EffDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Size = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Tbl_GemP__3214EC27195EBC95", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_MaterialCategory",
                columns: table => new
                {
                    MaterialID = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    MaterialName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Tbl_Mate__C50613171F3E1C81", x => x.MaterialID);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_Membership",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MinSpend = table.Column<double>(type: "float", nullable: true),
                    MaxSpend = table.Column<double>(type: "float", nullable: true),
                    DiscountRate = table.Column<double>(type: "float", nullable: true),
                    Ranking = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_Membership", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_ProductCategory",
                columns: table => new
                {
                    CategoryID = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    CategoryName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Tbl_Prod__19093A2BE43A65B1", x => x.CategoryID);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_Customer",
                columns: table => new
                {
                    CustomerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountID = table.Column<int>(type: "int", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Gender = table.Column<bool>(type: "bit", nullable: true),
                    Birthday = table.Column<DateTime>(type: "datetime", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Ranking = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    DiscountRate = table.Column<double>(type: "float", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: true),
                    Spending = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Tbl_Cust__A4AE64B85A5BC2D0", x => x.CustomerID);
                    table.ForeignKey(
                        name: "FK__Tbl_Custo__Accou__3C69FB99",
                        column: x => x.AccountID,
                        principalTable: "Tbl_Account",
                        principalColumn: "AccountID",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_SaleStaff",
                columns: table => new
                {
                    StaffID = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    AccountID = table.Column<int>(type: "int", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Tbl_Sale__96D4AAF7B4C34DBD", x => x.StaffID);
                    table.ForeignKey(
                        name: "FK__Tbl_SaleS__Accou__33D4B598",
                        column: x => x.AccountID,
                        principalTable: "Tbl_Account",
                        principalColumn: "AccountID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_Shipper",
                columns: table => new
                {
                    ShipperID = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    AccountID = table.Column<int>(type: "int", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Tbl_Ship__1F8AFFB94914BF77", x => x.ShipperID);
                    table.ForeignKey(
                        name: "FK__Tbl_Shipp__Accou__37A5467C",
                        column: x => x.AccountID,
                        principalTable: "Tbl_Account",
                        principalColumn: "AccountID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_DiamondGradingReport",
                columns: table => new
                {
                    ReportID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GemID = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: true),
                    GenerateDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Tbl_Diam__D5BD48E546D83403", x => x.ReportID);
                    table.ForeignKey(
                        name: "FK__Tbl_Diamo__GemID__5FB337D6",
                        column: x => x.GemID,
                        principalTable: "Tbl_Gem",
                        principalColumn: "GemID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_MaterialPriceList",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaterialID = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: true),
                    UnitPrice = table.Column<double>(type: "float", nullable: true),
                    EffDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Tbl_Mate__3214EC272938EEB3", x => x.ID);
                    table.ForeignKey(
                        name: "FK__Tbl_Mater__Mater__276EDEB3",
                        column: x => x.MaterialID,
                        principalTable: "Tbl_MaterialCategory",
                        principalColumn: "MaterialID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_Product",
                columns: table => new
                {
                    ProductID = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ProductCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CategoryID = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: true),
                    MaterialCost = table.Column<double>(type: "float", nullable: true),
                    GemCost = table.Column<double>(type: "float", nullable: true),
                    ProductionCost = table.Column<double>(type: "float", nullable: true),
                    PriceRate = table.Column<double>(type: "float", nullable: true),
                    ProductSize = table.Column<int>(type: "int", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: true),
                    UnitSizePrice = table.Column<double>(type: "float", nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Tbl_Prod__B40CC6EDE0D42E6F", x => x.ProductID);
                    table.ForeignKey(
                        name: "FK__Tbl_Produ__Categ__412EB0B6",
                        column: x => x.CategoryID,
                        principalTable: "Tbl_ProductCategory",
                        principalColumn: "CategoryID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_Order",
                columns: table => new
                {
                    OrderID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerID = table.Column<int>(type: "int", nullable: true),
                    OrderDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    PaymentMethod = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    OrderStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ShippingDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ReceiveDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    StaffID = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: true),
                    ShipperID = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: true),
                    OrderNote = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Tbl_Orde__C3905BAF45C74DFD", x => x.OrderID);
                    table.ForeignKey(
                        name: "FK__Tbl_Order__Custo__4E88ABD4",
                        column: x => x.CustomerID,
                        principalTable: "Tbl_Customer",
                        principalColumn: "CustomerID");
                    table.ForeignKey(
                        name: "FK__Tbl_Order__Shipp__5070F446",
                        column: x => x.ShipperID,
                        principalTable: "Tbl_Shipper",
                        principalColumn: "ShipperID");
                    table.ForeignKey(
                        name: "FK__Tbl_Order__Staff__4F7CD00D",
                        column: x => x.StaffID,
                        principalTable: "Tbl_SaleStaff",
                        principalColumn: "StaffID");
                });

            migrationBuilder.CreateTable(
                name: "Tbl_ProductGem",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductID = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: true),
                    GemID = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Tbl_Prod__3214EC2763B1E6B2", x => x.ID);
                    table.ForeignKey(
                        name: "FK__Tbl_Produ__GemID__4BAC3F29",
                        column: x => x.GemID,
                        principalTable: "Tbl_Gem",
                        principalColumn: "GemID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Tbl_Produ__Produ__4AB81AF0",
                        column: x => x.ProductID,
                        principalTable: "Tbl_Product",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_ProductMaterial",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductID = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: true),
                    MaterialID = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: true),
                    Weight = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Tbl_Prod__3214EC271DB74E5F", x => x.ID);
                    table.ForeignKey(
                        name: "FK__Tbl_Produ__Mater__45F365D3",
                        column: x => x.MaterialID,
                        principalTable: "Tbl_MaterialCategory",
                        principalColumn: "MaterialID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Tbl_Produ__Produ__44FF419A",
                        column: x => x.ProductID,
                        principalTable: "Tbl_Product",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_OrderDetail",
                columns: table => new
                {
                    OrderDetailID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderID = table.Column<int>(type: "int", nullable: true),
                    ProductID = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: true),
                    CustomizedSize = table.Column<int>(type: "int", nullable: true),
                    CustomizedAmount = table.Column<double>(type: "float", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: true),
                    TotalPrice = table.Column<double>(type: "float", nullable: true),
                    FinalPrice = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Tbl_Orde__D3B9D30CBDF411D5", x => x.OrderDetailID);
                    table.ForeignKey(
                        name: "FK__Tbl_Order__Order__534D60F1",
                        column: x => x.OrderID,
                        principalTable: "Tbl_Order",
                        principalColumn: "OrderID",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK__Tbl_Order__Produ__5441852A",
                        column: x => x.ProductID,
                        principalTable: "Tbl_Product",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_Payment",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderID = table.Column<int>(type: "int", nullable: true),
                    CustomerID = table.Column<int>(type: "int", nullable: true),
                    PaymentMethod = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TransactionID = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PayerEmail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Currency = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    PaymentStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PaymentDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Deposits = table.Column<double>(type: "float", nullable: true),
                    PayDetail = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Tbl_Paym__3214EC278AF50C7D", x => x.ID);
                    table.ForeignKey(
                        name: "FK__Tbl_Payme__Custo__17F790F9",
                        column: x => x.CustomerID,
                        principalTable: "Tbl_Customer",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK__Tbl_Payme__Order__17036CC0",
                        column: x => x.OrderID,
                        principalTable: "Tbl_Order",
                        principalColumn: "OrderID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_Warranty",
                columns: table => new
                {
                    WarrantyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderDetailID = table.Column<int>(type: "int", nullable: true),
                    WarrantyStartDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    WarrantyEndDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    WarrantyPdf = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Tbl_Warr__2ED318F3EBA9260C", x => x.WarrantyID);
                    table.ForeignKey(
                        name: "FK__Tbl_Warra__Order__5BE2A6F2",
                        column: x => x.OrderDetailID,
                        principalTable: "Tbl_OrderDetail",
                        principalColumn: "OrderDetailID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_Refund",
                columns: table => new
                {
                    RefundID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentID = table.Column<int>(type: "int", nullable: true),
                    RefundAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    RefundStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    RefundDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Tbl_Refu__725AB90042AC7B45", x => x.RefundID);
                    table.ForeignKey(
                        name: "FK__Tbl_Refun__Payme__1BC821DD",
                        column: x => x.PaymentID,
                        principalTable: "Tbl_Payment",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "UQ__Tbl_Acco__536C85E43F82C222",
                table: "Tbl_Account",
                column: "Username",
                unique: true,
                filter: "[Username] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UQ__Tbl_Cust__349DA58780491564",
                table: "Tbl_Customer",
                column: "AccountID",
                unique: true,
                filter: "[AccountID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UQ__Tbl_Diam__F101D5A116B098A6",
                table: "Tbl_DiamondGradingReport",
                column: "GemID",
                unique: true,
                filter: "[GemID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UQ__Tbl_Mate__C5061316CF12F515",
                table: "Tbl_MaterialPriceList",
                column: "MaterialID",
                unique: true,
                filter: "[MaterialID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Order_CustomerID",
                table: "Tbl_Order",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Order_ShipperID",
                table: "Tbl_Order",
                column: "ShipperID");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Order_StaffID",
                table: "Tbl_Order",
                column: "StaffID");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_OrderDetail_OrderID",
                table: "Tbl_OrderDetail",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_OrderDetail_ProductID",
                table: "Tbl_OrderDetail",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Payment_CustomerID",
                table: "Tbl_Payment",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "UQ__Tbl_Paym__C3905BAEE2026040",
                table: "Tbl_Payment",
                column: "OrderID",
                unique: true,
                filter: "[OrderID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Product_CategoryID",
                table: "Tbl_Product",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "UQ__Tbl_Prod__B40CC6ECC4F2941B",
                table: "Tbl_ProductGem",
                column: "ProductID",
                unique: true,
                filter: "[ProductID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UQ__Tbl_Prod__F101D5A1E13D6D16",
                table: "Tbl_ProductGem",
                column: "GemID",
                unique: true,
                filter: "[GemID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_ProductMaterial_MaterialID",
                table: "Tbl_ProductMaterial",
                column: "MaterialID");

            migrationBuilder.CreateIndex(
                name: "UQ__Tbl_Prod__B40CC6EC57E47CFD",
                table: "Tbl_ProductMaterial",
                column: "ProductID",
                unique: true,
                filter: "[ProductID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UQ__Tbl_Refu__9B556A5944646271",
                table: "Tbl_Refund",
                column: "PaymentID",
                unique: true,
                filter: "[PaymentID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UQ__Tbl_Sale__349DA587D74B32BF",
                table: "Tbl_SaleStaff",
                column: "AccountID",
                unique: true,
                filter: "[AccountID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UQ__Tbl_Ship__349DA587CB2371CC",
                table: "Tbl_Shipper",
                column: "AccountID",
                unique: true,
                filter: "[AccountID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UQ__Tbl_Warr__D3B9D30DB1A48541",
                table: "Tbl_Warranty",
                column: "OrderDetailID",
                unique: true,
                filter: "[OrderDetailID] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tbl_DiamondGradingReport");

            migrationBuilder.DropTable(
                name: "Tbl_GemPriceList");

            migrationBuilder.DropTable(
                name: "Tbl_MaterialPriceList");

            migrationBuilder.DropTable(
                name: "Tbl_Membership");

            migrationBuilder.DropTable(
                name: "Tbl_ProductGem");

            migrationBuilder.DropTable(
                name: "Tbl_ProductMaterial");

            migrationBuilder.DropTable(
                name: "Tbl_Refund");

            migrationBuilder.DropTable(
                name: "Tbl_Warranty");

            migrationBuilder.DropTable(
                name: "Tbl_Gem");

            migrationBuilder.DropTable(
                name: "Tbl_MaterialCategory");

            migrationBuilder.DropTable(
                name: "Tbl_Payment");

            migrationBuilder.DropTable(
                name: "Tbl_OrderDetail");

            migrationBuilder.DropTable(
                name: "Tbl_Order");

            migrationBuilder.DropTable(
                name: "Tbl_Product");

            migrationBuilder.DropTable(
                name: "Tbl_Customer");

            migrationBuilder.DropTable(
                name: "Tbl_Shipper");

            migrationBuilder.DropTable(
                name: "Tbl_SaleStaff");

            migrationBuilder.DropTable(
                name: "Tbl_ProductCategory");

            migrationBuilder.DropTable(
                name: "Tbl_Account");
        }
    }
}
