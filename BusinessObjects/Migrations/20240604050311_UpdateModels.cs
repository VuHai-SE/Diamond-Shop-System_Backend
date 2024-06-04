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
                    AccountID = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    Username = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Role = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Tbl_Acco__349DA5867ADF35A8", x => x.AccountID);
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
                    Origin = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CaratWeight = table.Column<double>(type: "float", nullable: true),
                    Color = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Cut = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Clarity = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Shape = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Tbl_Gem__F101D5A0CC383096", x => x.GemID);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_GemPriceList",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Origin = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
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
                    table.PrimaryKey("PK__Tbl_GemP__3214EC2708DD23BE", x => x.ID);
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
                    table.PrimaryKey("PK__Tbl_Mate__C50613171622D165", x => x.MaterialID);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_Membership",
                columns: table => new
                {
                    MinSpend = table.Column<double>(type: "float", nullable: true),
                    MaxSpend = table.Column<double>(type: "float", nullable: true),
                    DiscountRate = table.Column<double>(type: "float", nullable: true),
                    Ranking = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
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
                    table.PrimaryKey("PK__Tbl_Prod__19093A2B0200BE5A", x => x.CategoryID);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_Customer",
                columns: table => new
                {
                    CustomerID = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    AccountID = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Birthday = table.Column<DateTime>(type: "datetime", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Ranking = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    DiscountRate = table.Column<double>(type: "float", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Tbl_Cust__A4AE64B8C8CB26DA", x => x.CustomerID);
                    table.ForeignKey(
                        name: "FK__Tbl_Custo__Accou__36B12243",
                        column: x => x.AccountID,
                        principalTable: "Tbl_Account",
                        principalColumn: "AccountID",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_Staff",
                columns: table => new
                {
                    StaffID = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    AccountID = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Tbl_Staf__96D4AAF7001894A6", x => x.StaffID);
                    table.ForeignKey(
                        name: "FK__Tbl_Staff__Accou__33D4B598",
                        column: x => x.AccountID,
                        principalTable: "Tbl_Account",
                        principalColumn: "AccountID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_DiamondGradingReport",
                columns: table => new
                {
                    ReportID = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    GemID = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: true),
                    GenerateDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Tbl_Diam__D5BD48E5ACF3216E", x => x.ReportID);
                    table.ForeignKey(
                        name: "FK__Tbl_Diamo__GemID__5441852A",
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
                    table.PrimaryKey("PK__Tbl_Mate__3214EC27B4831432", x => x.ID);
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
                    Status = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Tbl_Prod__B40CC6EDC2BE8400", x => x.ProductID);
                    table.ForeignKey(
                        name: "FK__Tbl_Produ__Categ__3A81B327",
                        column: x => x.CategoryID,
                        principalTable: "Tbl_ProductCategory",
                        principalColumn: "CategoryID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_Order",
                columns: table => new
                {
                    OrderID = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    CustomerID = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: true),
                    OrderDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    PaymentMethod = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    OrderStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ShippingDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ReceiveDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    StaffID = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: true),
                    ShipperID = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Tbl_Orde__C3905BAFA3C9AEB3", x => x.OrderID);
                    table.ForeignKey(
                        name: "FK__Tbl_Order__Custo__44FF419A",
                        column: x => x.CustomerID,
                        principalTable: "Tbl_Customer",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK__Tbl_Order__Staff__45F365D3",
                        column: x => x.StaffID,
                        principalTable: "Tbl_Staff",
                        principalColumn: "StaffID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_ProductGem",
                columns: table => new
                {
                    ProductID = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    GemID = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Tbl_Prod__AB1CDBB7E43079B5", x => new { x.ProductID, x.GemID });
                    table.ForeignKey(
                        name: "FK__Tbl_Produ__GemID__4222D4EF",
                        column: x => x.GemID,
                        principalTable: "Tbl_Gem",
                        principalColumn: "GemID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Tbl_Produ__Produ__412EB0B6",
                        column: x => x.ProductID,
                        principalTable: "Tbl_Product",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_ProductMaterial",
                columns: table => new
                {
                    ProductID = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    MaterialID = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    Weight = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Tbl_Prod__D85CA7DC593BB93E", x => new { x.ProductID, x.MaterialID });
                    table.ForeignKey(
                        name: "FK__Tbl_Produ__Mater__3E52440B",
                        column: x => x.MaterialID,
                        principalTable: "Tbl_MaterialCategory",
                        principalColumn: "MaterialID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Tbl_Produ__Produ__3D5E1FD2",
                        column: x => x.ProductID,
                        principalTable: "Tbl_Product",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_OrderDetail",
                columns: table => new
                {
                    OrderDetailID = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    OrderID = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: true),
                    ProductID = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: true),
                    CustomizedSize = table.Column<int>(type: "int", nullable: true),
                    CustomizedAmount = table.Column<double>(type: "float", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: true),
                    TotalPrice = table.Column<double>(type: "float", nullable: true),
                    FinalPrice = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Tbl_Orde__D3B9D30C0DEBB22D", x => x.OrderDetailID);
                    table.ForeignKey(
                        name: "FK__Tbl_Order__Order__48CFD27E",
                        column: x => x.OrderID,
                        principalTable: "Tbl_Order",
                        principalColumn: "OrderID",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK__Tbl_Order__Produ__49C3F6B7",
                        column: x => x.ProductID,
                        principalTable: "Tbl_Product",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_Payment",
                columns: table => new
                {
                    OrderID = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: true),
                    CustomerID = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: true),
                    PaymentMethod = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Deposits = table.Column<double>(type: "float", nullable: true),
                    PayDetail = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK__Tbl_Payme__Custo__4CA06362",
                        column: x => x.CustomerID,
                        principalTable: "Tbl_Customer",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK__Tbl_Payme__Order__4BAC3F29",
                        column: x => x.OrderID,
                        principalTable: "Tbl_Order",
                        principalColumn: "OrderID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_Warranty",
                columns: table => new
                {
                    WarrantyID = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    OrderDetailID = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: true),
                    WarrantyStartDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    WarrantyEndDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Tbl_Warr__2ED318F3294E9FAC", x => x.WarrantyID);
                    table.ForeignKey(
                        name: "FK__Tbl_Warra__Order__5070F446",
                        column: x => x.OrderDetailID,
                        principalTable: "Tbl_OrderDetail",
                        principalColumn: "OrderDetailID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "UQ__Tbl_Acco__536C85E44347055E",
                table: "Tbl_Account",
                column: "Username",
                unique: true,
                filter: "[Username] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Customer_AccountID",
                table: "Tbl_Customer",
                column: "AccountID");

            migrationBuilder.CreateIndex(
                name: "UQ__Tbl_Diam__F101D5A15BF1F2BE",
                table: "Tbl_DiamondGradingReport",
                column: "GemID",
                unique: true,
                filter: "[GemID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UQ__Tbl_Mate__C50613168298B295",
                table: "Tbl_MaterialPriceList",
                column: "MaterialID",
                unique: true,
                filter: "[MaterialID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Order_CustomerID",
                table: "Tbl_Order",
                column: "CustomerID");

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
                name: "IX_Tbl_Payment_OrderID",
                table: "Tbl_Payment",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Product_CategoryID",
                table: "Tbl_Product",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_ProductGem_GemID",
                table: "Tbl_ProductGem",
                column: "GemID");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_ProductMaterial_MaterialID",
                table: "Tbl_ProductMaterial",
                column: "MaterialID");

            migrationBuilder.CreateIndex(
                name: "UQ__Tbl_Staf__349DA5876E727721",
                table: "Tbl_Staff",
                column: "AccountID",
                unique: true,
                filter: "[AccountID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UQ__Tbl_Warr__D3B9D30DC4EE2FAF",
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
                name: "Tbl_Payment");

            migrationBuilder.DropTable(
                name: "Tbl_ProductGem");

            migrationBuilder.DropTable(
                name: "Tbl_ProductMaterial");

            migrationBuilder.DropTable(
                name: "Tbl_Warranty");

            migrationBuilder.DropTable(
                name: "Tbl_Gem");

            migrationBuilder.DropTable(
                name: "Tbl_MaterialCategory");

            migrationBuilder.DropTable(
                name: "Tbl_OrderDetail");

            migrationBuilder.DropTable(
                name: "Tbl_Order");

            migrationBuilder.DropTable(
                name: "Tbl_Product");

            migrationBuilder.DropTable(
                name: "Tbl_Customer");

            migrationBuilder.DropTable(
                name: "Tbl_Staff");

            migrationBuilder.DropTable(
                name: "Tbl_ProductCategory");

            migrationBuilder.DropTable(
                name: "Tbl_Account");
        }
    }
}
