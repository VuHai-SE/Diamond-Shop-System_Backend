-- Drop Database if it exists
use master
IF EXISTS (SELECT * FROM sys.databases WHERE name = 'DiamondStore')
BEGIN
    DROP DATABASE DiamondStore;
END
GO

-- Create Database
CREATE DATABASE DiamondStore;
GO

USE DiamondStore;
GO

-- Create tables
CREATE TABLE Tbl_MaterialCategory (
    MaterialID NVARCHAR(8) PRIMARY KEY,
    MaterialName NVARCHAR(100)
);

CREATE TABLE Tbl_MaterialPriceList (
	ID INT IDENTITY(1,1) PRIMARY KEY,
    MaterialID NVARCHAR(8) UNIQUE,
    UnitPrice FLOAT,
    EffDate DATETIME,
    FOREIGN KEY (MaterialID) REFERENCES Tbl_MaterialCategory(MaterialID) ON DELETE CASCADE
);

CREATE TABLE Tbl_GemPriceList (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    Origin BIT,
    CaratWeight FLOAT,
    Color NVARCHAR(50),
    Cut NVARCHAR(50),
    Clarity NVARCHAR(50),
    Price FLOAT,
	EffDate DATETIME,
	Size FLOAT
);

CREATE TABLE Tbl_Gem (
    GemID NVARCHAR(8) PRIMARY KEY,
    GemName NVARCHAR(50),
    Polish NVARCHAR(50),
    Symmetry NVARCHAR(50),
    Fluorescence NVARCHAR(50),
    Origin BIT,
    CaratWeight FLOAT,
    Color NVARCHAR(50),
    Cut NVARCHAR(50),
    Clarity NVARCHAR(50),
    Shape NVARCHAR(50)
);

CREATE TABLE Tbl_ProductCategory (
    CategoryID NVARCHAR(8) PRIMARY KEY,
    CategoryName NVARCHAR(100)
);

CREATE TABLE Tbl_Account (
    AccountID INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(100) UNIQUE,
    Password NVARCHAR(100),
    Role NVARCHAR(50)
);

CREATE TABLE Tbl_SaleStaff (
    StaffID NVARCHAR(8) PRIMARY KEY,
    AccountID INT UNIQUE,
    FirstName NVARCHAR(100),
    LastName NVARCHAR(100),
    FOREIGN KEY (AccountID) REFERENCES Tbl_Account(AccountID) ON DELETE CASCADE
);

CREATE TABLE Tbl_Shipper (
    ShipperID NVARCHAR(8) PRIMARY KEY,
    AccountID INT UNIQUE,
    FirstName NVARCHAR(100),
    LastName NVARCHAR(100),
    FOREIGN KEY (AccountID) REFERENCES Tbl_Account(AccountID) ON DELETE CASCADE
);

CREATE TABLE Tbl_Customer (
    CustomerID INT IDENTITY(1,1) PRIMARY KEY,
    AccountID INT UNIQUE,
    FirstName NVARCHAR(100),
    LastName NVARCHAR(100),
    Gender BIT,
    Birthday DATETIME,
    Email NVARCHAR(100),
    PhoneNumber NVARCHAR(10),
    Address NVARCHAR(200),
    Ranking NVARCHAR(10),
    DiscountRate FLOAT,
    Status BIT,
	Spending DECIMAL(18, 2) NOT NULL DEFAULT 0,
    FOREIGN KEY (AccountID) REFERENCES Tbl_Account(AccountID) ON DELETE SET NULL
);
---------------------------------------------------
CREATE TABLE Tbl_Membership (
    MinSpend FLOAT,
    MaxSpend FLOAT,
    DiscountRate FLOAT,
    Ranking NVARCHAR(50)
);
ALTER TABLE Tbl_Membership
ADD ID INT IDENTITY(1,1);

ALTER TABLE Tbl_Membership
ADD CONSTRAINT PK_Tbl_Membership PRIMARY KEY (ID);
---------------------------------------------------
CREATE TABLE Tbl_Product (
    ProductID NVARCHAR(8) PRIMARY KEY,
    ProductName NVARCHAR(100),
    ProductCode NVARCHAR(50),
    Description NVARCHAR(255),
    CategoryID NVARCHAR(8),
    MaterialCost FLOAT,
    GemCost FLOAT,
    ProductionCost FLOAT,
    PriceRate FLOAT,
    ProductSize INT,
    Image NVARCHAR(255),
    Status BIT,
    UnitSizePrice FLOAT,
	Gender INT, --  -1 Female, 1 Male, 0 Both
    FOREIGN KEY (CategoryID) REFERENCES Tbl_ProductCategory(CategoryID) ON DELETE CASCADE
);

CREATE TABLE Tbl_ProductMaterial (
	ID INT IDENTITY(1,1) PRIMARY KEY,
    ProductID NVARCHAR(8) UNIQUE,
    MaterialID NVARCHAR(8),
    Weight FLOAT,
    FOREIGN KEY (ProductID) REFERENCES Tbl_Product(ProductID) ON DELETE CASCADE,
    FOREIGN KEY (MaterialID) REFERENCES Tbl_MaterialCategory(MaterialID) ON DELETE CASCADE
);

-- Tbl_ProductGem
CREATE TABLE Tbl_ProductGem (
	ID INT IDENTITY(1,1) PRIMARY KEY,
    ProductID NVARCHAR(8) UNIQUE,
    GemID NVARCHAR(8) UNIQUE,
    FOREIGN KEY (ProductID) REFERENCES Tbl_Product(ProductID) ON DELETE CASCADE,
    FOREIGN KEY (GemID) REFERENCES Tbl_Gem(GemID) ON DELETE CASCADE
);

CREATE TABLE Tbl_Order (
    OrderID INT IDENTITY(1,1) PRIMARY KEY,
    CustomerID INT,--THAY DOI KIEUR DU LIEU
    OrderDate DATETIME,
    PaymentMethod NVARCHAR(50),
    OrderStatus NVARCHAR(50),
    ShippingDate DATETIME,
    ReceiveDate DATETIME,
    StaffID NVARCHAR(8),
    ShipperID NVARCHAR(8),
	OrderNote NVARCHAR(50),
    FOREIGN KEY (CustomerID) REFERENCES Tbl_Customer(CustomerID),
    FOREIGN KEY (StaffID) REFERENCES Tbl_SaleStaff(StaffID),
    FOREIGN KEY (ShipperID) REFERENCES Tbl_Shipper(ShipperID)
);

CREATE TABLE Tbl_OrderDetail (
    OrderDetailID INT IDENTITY(1,1) PRIMARY KEY,
    OrderID INT,
    ProductID NVARCHAR(8),
    CustomizedSize INT,
    CustomizedAmount FLOAT,
	Quantity INT,
    TotalPrice FLOAT,
    FinalPrice FLOAT,
    FOREIGN KEY (OrderID) REFERENCES Tbl_Order(OrderID) ON DELETE SET NULL,
    FOREIGN KEY (ProductID) REFERENCES Tbl_Product(ProductID) ON DELETE CASCADE
);

CREATE TABLE Tbl_Payment (
    ID INT PRIMARY KEY IDENTITY(1,1),
    OrderID INT UNIQUE,
    CustomerID INT,
    PaymentMethod NVARCHAR(50),
    TransactionID NVARCHAR(100),
    PayerEmail NVARCHAR(100),
    Amount DECIMAL(18, 2),
    Currency NVARCHAR(10),
    PaymentStatus NVARCHAR(50),
    PaymentDate DATETIME,
    Deposits FLOAT,
    PayDetail NVARCHAR(255),
    FOREIGN KEY (OrderID) REFERENCES Tbl_Order(OrderID) ON DELETE CASCADE,
    FOREIGN KEY (CustomerID) REFERENCES Tbl_Customer(CustomerID) ON DELETE SET NULL
);

CREATE TABLE Tbl_Refund (
    RefundID INT PRIMARY KEY IDENTITY(1,1),
    PaymentID INT UNIQUE,
    RefundAmount DECIMAL(18, 2),
    RefundStatus NVARCHAR(50),
    RefundDate DATETIME,
    Reason NVARCHAR(MAX),
    FOREIGN KEY (PaymentID) REFERENCES Tbl_Payment(ID),
);


CREATE TABLE Tbl_Warranty (
    WarrantyID INT IDENTITY(1,1) PRIMARY KEY,
    OrderDetailID INT UNIQUE,
    WarrantyStartDate DATETIME,
    WarrantyEndDate DATETIME,
	WarrantyPdf VARBINARY(MAX),
    FOREIGN KEY (OrderDetailID) REFERENCES Tbl_OrderDetail(OrderDetailID) ON DELETE CASCADE
);

CREATE TABLE Tbl_DiamondGradingReport (
    ReportID INT IDENTITY(1,1) PRIMARY KEY,
    GemID NVARCHAR(8) UNIQUE,
    GenerateDate DATETIME,
    Image NVARCHAR(255),
    FOREIGN KEY (GemID) REFERENCES Tbl_Gem(GemID) ON DELETE CASCADE
);
