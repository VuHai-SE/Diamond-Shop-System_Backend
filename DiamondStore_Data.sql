USE DiamondStore;
GO

-- Insert sample data into Tbl_MaterialCategory
INSERT INTO Tbl_MaterialCategory (MaterialID, MaterialName)
VALUES 
('M001', 'Gold'),
('M002', 'Silver'),
('M003', 'Platinum'),
('M004', 'Palladium'),
('M005', 'Titanium');

-- Insert sample data into Tbl_MaterialPriceList
INSERT INTO Tbl_MaterialPriceList (MaterialID, UnitPrice, EffDate)
VALUES 
('M001', 60.0, '2024-01-01'),
('M002', 25.0, '2024-01-01'),
('M003', 100.0, '2024-01-01'),
('M004', 70.0, '2024-01-01'),
('M005', 50.0, '2024-01-01');

-- Insert sample data into Tbl_GemPriceList
INSERT INTO Tbl_GemPriceList (Origin, CaratWeight, Color, Cut, Clarity, Price, EffDate, Size)
VALUES 
('South Africa', 1.0, 'D', 'Excellent', 'IF', 10000.0, '2024-01-01', 5.0),
('Russia', 0.8, 'E', 'Very Good', 'VVS1', 8000.0, '2024-01-01', 4.0),
('Australia', 0.5, 'F', 'Good', 'VS1', 5000.0, '2024-01-01', 3.0),
('Canada', 1.2, 'G', 'Excellent', 'SI1', 12000.0, '2024-01-01', 6.0),
('Brazil', 0.9, 'H', 'Very Good', 'I1', 9000.0, '2024-01-01', 4.5);

-- Insert sample data into Tbl_Gem
INSERT INTO Tbl_Gem (GemID, GemName, Polish, Symmetry, Fluorescence, Origin, CaratWeight, Color, Cut, Clarity, Shape)
VALUES 
('G001', 'Diamond', 'Excellent', 'Excellent', 'None', 'South Africa', 1.0, 'D', 'Excellent', 'IF', 'Round'),
('G002', 'Diamond', 'Very Good', 'Very Good', 'Faint', 'Russia', 0.8, 'E', 'Very Good', 'VVS1', 'Princess'),
('G003', 'Diamond', 'Good', 'Good', 'Medium', 'Australia', 0.5, 'F', 'Good', 'VS1', 'Oval'),
('G004', 'Diamond', 'Excellent', 'Excellent', 'Strong', 'Canada', 1.2, 'G', 'Excellent', 'SI1', 'Cushion'),
('G005', 'Diamond', 'Very Good', 'Very Good', 'None', 'Brazil', 0.9, 'H', 'Very Good', 'I1', 'Emerald');

-- Insert sample data into Tbl_ProductCategory
INSERT INTO Tbl_ProductCategory (CategoryID, CategoryName)
VALUES 
('C001', 'Rings'),
('C002', 'Necklaces'),
('C003', 'Bracelets'),
('C004', 'Earrings'),
('C005', 'Watches');

-- Insert sample data into Tbl_Account
INSERT INTO Tbl_Account (Username, Password, Role)
VALUES 
('johndoe', 'password123', 'Customer'), --1
('janedoe', 'password123', 'Customer'), --2
('staff1', 'password123', 'SaleStaff'), --3
('admin1', 'password123', 'Admin'), --4
('staff2', 'password123', 'SaleStaff'), --5
('shipper1', 'password123', 'Shipper'),--6
('shipper2', 'password123', 'Shipper'),--7
('tom', 'password123', 'Customer'),--8
('lisa', 'password123', 'Customer'),--9
('mark', 'password123', 'Customer');--10

-- Insert sample data into Tbl_Staff
INSERT INTO Tbl_SaleStaff (StaffID, AccountID, FirstName, LastName)
VALUES 
('S001', 3, 'Alice', 'Smith'),
('S002', 5, 'Bob', 'Johnson');

INSERT INTO Tbl_Shipper(ShipperID, AccountID, FirstName, LastName)
VALUES 
('SP001', 9, 'Jack', 'Grealish'),
('SP002', 10, 'Marcus', 'Rashford');


-- Insert sample data into Tbl_Customer
INSERT INTO Tbl_Customer (CustomerID, AccountID, FirstName, LastName, Gender, Birthday, Email, PhoneNumber, Address, Ranking, DiscountRate, Status)
VALUES 
('C001', 1, 'John', 'Doe', 'Male', '1990-01-01', 'john.doe@example.com', '0123456789', '123 Main St', 'Silver', 5.0, 1),
('C002', 2, 'Jane', 'Doe', 'Female', '1992-02-02', 'jane.doe@example.com', '0987654321', '456 Elm St', 'Gold', 10.0, 1),
('C003', 8, 'Tom', 'Smith', 'Male', '1985-03-03', 'tom.smith@example.com', '0123456788', '789 Pine St', 'Bronze', 2.0, 1),
('C004', 9, 'Lisa', 'Brown', 'Female', '1988-04-04', 'lisa.brown@example.com', '0987654320', '101 Oak St', 'Platinum', 15.0, 1),
('C005', 10, 'Mark', 'Davis', 'Male', '1995-05-05', 'mark.davis@example.com', '0123456787', '202 Cedar St', 'Silver', 5.0, 1);

-- Insert sample data into Tbl_Membership
INSERT INTO Tbl_Membership (MinSpend, MaxSpend, DiscountRate, Ranking)
VALUES 
(0.0, 999.99, 2.0, 'Bronze'),
(1000.0, 4999.99, 5.0, 'Silver'),
(5000.0, 9999.99, 10.0, 'Gold'),
(10000.0, 19999.99, 15.0, 'Platinum'),
(20000.0, NULL, 20.0, 'Diamond');

-- Insert sample data into Tbl_Product
INSERT INTO Tbl_Product (ProductID, ProductName, ProductCode, Description, CategoryID, MaterialCost, GemCost, ProductionCost, PriceRate, ProductSize, Image, Status)
VALUES 
('P001', 'Gold Ring', 'R001', '18k Gold Ring', 'C001', 100.0, 200.0, 50.0, 1.5, 7, 'ring1.jpg', 1),
('P002', 'Silver Necklace', 'N001', 'Sterling Silver Necklace', 'C002', 50.0, 0.0, 30.0, 1.4, 18, 'necklace1.jpg', 1),
('P003', 'Platinum Bracelet', 'B001', 'Platinum Bracelet', 'C003', 200.0, 0.0, 70.0, 1.6, 8, 'bracelet1.jpg', 1),
('P004', 'Diamond Earrings', 'E001', 'Diamond Earrings', 'C004', 100.0, 1000.0, 80.0, 1.8, 4, 'earrings1.jpg', 1),
('P005', 'Titanium Watch', 'W001', 'Titanium Watch', 'C005', 150.0, 0.0, 100.0, 1.7, 42, 'watch1.jpg', 1);

-- Insert sample data into Tbl_ProductMaterial
INSERT INTO Tbl_ProductMaterial (ProductID, MaterialID, Weight)
VALUES 
('P001', 'M001', 5.0),
('P002', 'M002', 10.0),
('P003', 'M003', 8.0),
('P004', 'M004', 6.0),
('P005', 'M005', 7.0);

-- Insert sample data into Tbl_ProductGem
INSERT INTO Tbl_ProductGem (ProductID, GemID)
VALUES 
('P001', 'G001'),
('P004', 'G002');

-- Insert sample data into Tbl_Order
INSERT INTO Tbl_Order (CustomerID, OrderDate, PaymentMethod, OrderStatus, ShippingDate, ReceiveDate, StaffID, ShipperID, ShipStatus)
VALUES 
('C001', '2024-05-01', 'Credit Card', 'Delivered', '2024-05-02', '2024-05-05', 'S001', 'SP001', 'Done'),
('C002', '2024-05-02', 'Paypal', 'Delivered', '2024-05-03', '2024-05-06', 'S002', 'SP002', 'Done'),
('C003', '2024-05-03', 'Cash on Delivery', 'Processing', NULL, NULL, 'S001', 'SP002', 'Pending'),
('C004', '2024-05-04', 'Credit Card', 'Delivered', '2024-05-05', '2024-05-08', 'S002', 'SP001', 'Done'),
('C005', '2024-05-05', 'Bank Transfer', 'Delivered', '2024-05-06', '2024-05-09', 'S001', 'SP002', 'Done');


-- Insert sample data into Tbl_OrderDetail
INSERT INTO Tbl_OrderDetail (OrderID, ProductID, CustomizedSize, CustomizedAmount, Quantity, TotalPrice, FinalPrice)
VALUES 
(1, 'P001', 8, 5.0, 1, 450.0, 427.5),
(2, 'P002', 18, 10.0, 2, 200.0, 180.0),
(3, 'P003', 9, 8.0, 1, 432.0, 421.0),
(4, 'P004', 4, 6.0, 1, 2160.0, 1836.0),
(5, 'P005', 42, 7.0, 1, 425.0, 382.5);

-- Insert sample data into Tbl_Payment
INSERT INTO Tbl_Payment (OrderID, CustomerID, PaymentMethod, Deposits, PayDetail)
VALUES 
(1, 'C001', 'Credit Card', 100.0, 'Paid in full'),
(2, 'C002', 'Paypal', 50.0, 'Paid in full'),
(3, 'C003', 'Cash on Delivery', 0.0, 'To be paid upon delivery'),
(4, 'C004', 'Credit Card', 100.0, 'Paid in full'),
(5, 'C005', 'Bank Transfer', 150.0, 'Paid in full');

-- Insert sample data into Tbl_Warranty
INSERT INTO Tbl_Warranty (WarrantyID, OrderDetailID, WarrantyStartDate, WarrantyEndDate)
VALUES 
('W001', 1, '2024-05-05', '2025-05-05'),
('W002', 2, '2024-05-07', '2025-05-07'),
('W003', 3, '2024-05-09', '2025-05-09'),
('W004', 4, '2024-05-11', '2025-05-11'),
('W005', 5, '2024-05-13', '2025-05-13');

-- Insert sample data into Tbl_DiamondGradingReport
INSERT INTO Tbl_DiamondGradingReport (GemID, GenerateDate, Image)
VALUES 
('G001', '2024-01-01', 'report1.jpg'),
('G002', '2024-01-02', 'report2.jpg'),
('G003', '2024-01-03', 'report3.jpg'),
('G004', '2024-01-04', 'report4.jpg'),
('G005', '2024-01-05', 'report5.jpg');

INSERT INTO Tbl_Product (ProductID, ProductName, ProductCode, Description, CategoryID, MaterialCost, GemCost, ProductionCost, PriceRate, ProductSize, Image, Status)
VALUES 
('P006', 'High Jewellery Necklace', 'N006', 'High Jewellery Necklace', 'C002', 500.0, 1000.0, 300.0, 2.0, 18, 'high_jewellery_necklace.png', 1),
('P007', 'White Gold Cartier Necklace', 'N007', 'White Gold Cartier Necklace', 'C002', 400.0, 800.0, 250.0, 1.9, 18, 'white_gold_cartier_necklace.png', 1),
('P008', 'High Jewellery Necklace', 'N008', 'High Jewellery Necklace', 'C002', 550.0, 1100.0, 320.0, 2.1, 18, 'high_jewellery_necklace_2.png', 1),
('P009', 'White Gold Cartier Necklace', 'N009', 'White Gold Cartier Necklace', 'C002', 450.0, 900.0, 270.0, 1.95, 18, 'white_gold_cartier_necklace_2.png', 1),
('P010', 'Creative Collection Necklace', 'N010', 'Creative Collection Necklace', 'C002', 600.0, 1200.0, 350.0, 2.2, 18, 'creative_collection_necklace.png', 1),
('P011', 'Cactus Cartier Necklace', 'N011', 'Cactus Cartier Necklace', 'C002', 700.0, 1400.0, 400.0, 2.3, 18, 'cactus_cartier_necklace.png', 1),
('P012', 'Diamond Collection Necklace', 'N012', 'Diamond Collection Necklace', 'C002', 750.0, 1500.0, 420.0, 2.4, 18, 'diamond_collection_necklace.png', 1),
('P013', 'Essential Lines Necklace', 'N013', 'Essential Lines Necklace', 'C002', 650.0, 1300.0, 370.0, 2.25, 18, 'essential_lines_necklace.png', 1),
('P014', 'High Jewellery Necklace', 'N014', 'High Jewellery Necklace', 'C002', 800.0, 1600.0, 450.0, 2.5, 18, 'high_jewellery_necklace_3.png', 1),
('P015', 'Pluie Cartier Necklace', 'N015', 'Pluie Cartier Necklace', 'C002', 850.0, 1700.0, 480.0, 2.55, 18, 'pluie_cartier_necklace.png', 1),
('P016', 'Rose Gold Rows', 'N016', 'Rose Gold Rows', 'C002', 900.0, 1800.0, 500.0, 2.6, 18, 'rose_gold_rows.png', 1),
('P017', 'High Jewellery Necklace', 'N017', 'High Jewellery Necklace', 'C002', 950.0, 1900.0, 530.0, 2.65, 18, 'high_jewellery_necklace_4.png', 1),
('P018', 'High Jewellery Necklace', 'N018', 'High Jewellery Necklace', 'C002', 1000.0, 2000.0, 550.0, 2.7, 18, 'high_jewellery_necklace_5.png', 1),
('P019', 'Essential Lines Necklace', 'N019', 'Essential Lines Necklace', 'C002', 1050.0, 2100.0, 580.0, 2.75, 18, 'essential_lines_necklace_2.png', 1),
('P020', 'Creative Collection Necklace', 'N020', 'Creative Collection Necklace', 'C002', 1100.0, 2200.0, 600.0, 2.8, 18, 'creative_collection_necklace_2.png', 1),
('P021', 'Natural Necklace', 'N021', 'Natural Necklace', 'C002', 1150.0, 2300.0, 620.0, 2.85, 18, 'natural_necklace.png', 1),
('P022', 'White Gold Cartier Necklace', 'N022', 'White Gold Cartier Necklace', 'C002', 1200.0, 2400.0, 650.0, 2.9, 18, 'white_gold_cartier_necklace_3.png', 1),
('P023', 'High Jewellery Necklace', 'N023', 'High Jewellery Necklace', 'C002', 1250.0, 2500.0, 680.0, 2.95, 18, 'high_jewellery_necklace_6.png', 1),
('P024', 'Cactus Cartier Necklace', 'N024', 'Cactus Cartier Necklace', 'C002', 1300.0, 2600.0, 700.0, 3.0, 18, 'cactus_cartier_necklace_2.png', 1);
