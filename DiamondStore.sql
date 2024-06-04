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
    Origin NVARCHAR(100),
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
    Origin NVARCHAR(100),
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
    AccountID NVARCHAR(8) PRIMARY KEY,
    Username NVARCHAR(100) UNIQUE,
    Password NVARCHAR(100),
    Role NVARCHAR(50)
);

CREATE TABLE Tbl_Staff (
    StaffID NVARCHAR(8) PRIMARY KEY,
    AccountID NVARCHAR(8) UNIQUE,
    FirstName NVARCHAR(100),
    LastName NVARCHAR(100),
    FOREIGN KEY (AccountID) REFERENCES Tbl_Account(AccountID) ON DELETE CASCADE
);

CREATE TABLE Tbl_Customer (
    CustomerID NVARCHAR(8) PRIMARY KEY,
    AccountID NVARCHAR(8),
    FirstName NVARCHAR(100),
    LastName NVARCHAR(100),
    Gender NVARCHAR(10),
    Birthday DATETIME,
    Email NVARCHAR(100),
    PhoneNumber NVARCHAR(10),
    Address NVARCHAR(200),
    Ranking NVARCHAR(10),
	DiscountRate FLOAT,
    Status BIT,
    FOREIGN KEY (AccountID) REFERENCES Tbl_Account(AccountID) ON DELETE SET NULL
);

CREATE TABLE Tbl_Membership (
    MinSpend FLOAT,
    MaxSpend FLOAT,
    DiscountRate FLOAT,
    Ranking NVARCHAR(50)
);

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
    FOREIGN KEY (CategoryID) REFERENCES Tbl_ProductCategory(CategoryID) ON DELETE CASCADE,
);

CREATE TABLE Tbl_ProductMaterial (
    ProductID NVARCHAR(8),
    MaterialID NVARCHAR(8),
    Weight FLOAT,
    PRIMARY KEY (ProductID, MaterialID),
    FOREIGN KEY (ProductID) REFERENCES Tbl_Product(ProductID) ON DELETE CASCADE,
    FOREIGN KEY (MaterialID) REFERENCES Tbl_MaterialCategory(MaterialID) ON DELETE CASCADE
);

-- Tbl_ProductGem
CREATE TABLE Tbl_ProductGem (
    ProductID NVARCHAR(8),
    GemID NVARCHAR(8),
    PRIMARY KEY (ProductID, GemID),
    FOREIGN KEY (ProductID) REFERENCES Tbl_Product(ProductID) ON DELETE CASCADE,
    FOREIGN KEY (GemID) REFERENCES Tbl_Gem(GemID) ON DELETE CASCADE
);

CREATE TABLE Tbl_Order (
    OrderID NVARCHAR(8) PRIMARY KEY,
    CustomerID NVARCHAR(8),
    OrderDate DATETIME,
    PaymentMethod NVARCHAR(50),
    OrderStatus NVARCHAR(50),
    ShippingDate DATETIME,
    ReceiveDate DATETIME,
    StaffID NVARCHAR(8),
    ShipperID NVARCHAR(8),
    FOREIGN KEY (CustomerID) REFERENCES Tbl_Customer(CustomerID) ON DELETE SET NULL,
    FOREIGN KEY (StaffID) REFERENCES Tbl_Staff(StaffID) ON DELETE CASCADE
);

CREATE TABLE Tbl_OrderDetail (
    OrderDetailID NVARCHAR(8) PRIMARY KEY,
    OrderID NVARCHAR(8),
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
    OrderID NVARCHAR(8),
    CustomerID NVARCHAR(8),
    PaymentMethod NVARCHAR(50),
    Deposits FLOAT,
    PayDetail NVARCHAR(255),
    --PRIMARY KEY (OrderID, CustomerID),
    FOREIGN KEY (OrderID) REFERENCES Tbl_Order(OrderID) ON DELETE CASCADE,
    FOREIGN KEY (CustomerID) REFERENCES Tbl_Customer(CustomerID) ON DELETE SET NULL
);

CREATE TABLE Tbl_Warranty (
    WarrantyID NVARCHAR(8) PRIMARY KEY,
    OrderDetailID NVARCHAR(8) UNIQUE,
    WarrantyStartDate DATETIME,
    WarrantyEndDate DATETIME,
    FOREIGN KEY (OrderDetailID) REFERENCES Tbl_OrderDetail(OrderDetailID) ON DELETE CASCADE
);

CREATE TABLE Tbl_DiamondGradingReport (
    ReportID NVARCHAR(8) PRIMARY KEY,
    GemID NVARCHAR(8) UNIQUE,
    GenerateDate DATETIME,
    Image NVARCHAR(255),
    FOREIGN KEY (GemID) REFERENCES Tbl_Gem(GemID) ON DELETE CASCADE
);
