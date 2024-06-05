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
INSERT INTO Tbl_Order (OrderID, CustomerID, OrderDate, PaymentMethod, OrderStatus, ShippingDate, ReceiveDate, StaffID, ShipperID, ShipStatus)
VALUES 
('O001', 'C001', '2024-05-01', 'Credit Card', 'Delivered', '2024-05-02', '2024-05-05', 'S001', 'SP001', 'Done'),
('O002', 'C002', '2024-05-02', 'Paypal', 'Delivered', '2024-05-03', '2024-05-06', 'S002', 'SP002', 'Done'),
('O003', 'C003', '2024-05-03', 'Cash on Delivery', 'Processing', NULL, NULL, 'S001', 'SP002', 'Pending'),
('O004', 'C004', '2024-05-04', 'Credit Card', 'Delivered', '2024-05-05', '2024-05-08', 'S002', 'SP001', 'Done'),
('O005', 'C005', '2024-05-05', 'Bank Transfer', 'Delivered', '2024-05-06', '2024-05-09', 'S001', 'SP002', 'Done');


-- Insert sample data into Tbl_OrderDetail
INSERT INTO Tbl_OrderDetail (OrderDetailID, OrderID, ProductID, CustomizedSize, CustomizedAmount, Quantity, TotalPrice, FinalPrice)
VALUES 
('OD001', 'O001', 'P001', 8, 5.0, 1, 450.0, 427.5),
('OD002', 'O002', 'P002', 18, 10.0, 2, 200.0, 180.0),
('OD003', 'O003', 'P003', 9, 8.0, 1, 432.0, 421.0),
('OD004', 'O004', 'P004', 4, 6.0, 1, 2160.0, 1836.0),
('OD005', 'O005', 'P005', 42, 7.0, 1, 425.0, 382.5);

-- Insert sample data into Tbl_Payment
INSERT INTO Tbl_Payment (OrderID, CustomerID, PaymentMethod, Deposits, PayDetail)
VALUES 
('O001', 'C001', 'Credit Card', 100.0, 'Paid in full'),
('O002', 'C002', 'Paypal', 50.0, 'Paid in full'),
('O003', 'C003', 'Cash on Delivery', 0.0, 'To be paid upon delivery'),
('O004', 'C004', 'Credit Card', 100.0, 'Paid in full'),
('O005', 'C005', 'Bank Transfer', 150.0, 'Paid in full');

-- Insert sample data into Tbl_Warranty
INSERT INTO Tbl_Warranty (WarrantyID, OrderDetailID, WarrantyStartDate, WarrantyEndDate)
VALUES 
('W001', 'OD001', '2024-05-05', '2025-05-05'),
('W002', 'OD002', '2024-05-07', '2025-05-07'),
('W003', 'OD003', '2024-05-09', '2025-05-09'),
('W004', 'OD004', '2024-05-11', '2025-05-11'),
('W005', 'OD005', '2024-05-13', '2025-05-13');

-- Insert sample data into Tbl_DiamondGradingReport
INSERT INTO Tbl_DiamondGradingReport (ReportID, GemID, GenerateDate, Image)
VALUES 
('R001', 'G001', '2024-01-01', 'report1.jpg'),
('R002', 'G002', '2024-01-02', 'report2.jpg'),
('R003', 'G003', '2024-01-03', 'report3.jpg'),
('R004', 'G004', '2024-01-04', 'report4.jpg'),
('R005', 'G005', '2024-01-05', 'report5.jpg');

Insert into Tbl_Product(Image)
values
('HIGH JEWELLERY NECKLACE', 'https://www.cartier.com/dw/image/v2/BGTJ_PRD/on/demandware.static/-/Sites-cartier-master/default/dw47181d40/images/large/0173c18084d351718d0a9e48cbf37bf4.png?sw=350&sh=350&sm=fit&sfrm=png'),
('WHITE GOLD CARTIER NECKLACE', 'https://www.cartier.com/dw/image/v2/BGTJ_PRD/on/demandware.static/-/Sites-cartier-master/default/dwffda1a5e/images/large/a7cde65b34e85f8d81ac915d458c5f9c.png?sw=350&sh=350&sm=fit&sfrm=png'),
('HIGH JEWELLERY NECKLACE', 'https://www.cartier.com/dw/image/v2/BGTJ_PRD/on/demandware.static/-/Sites-cartier-master/default/dw1a70f15f/images/large/f1d14e7bda6653d094c29e0e8e0c255d.png?sw=750&sh=750&sm=fit&sfrm=png'),
('WHITE GOLD CARTIER NECKLACE' , 'https://www.cartier.com/dw/image/v2/BGTJ_PRD/on/demandware.static/-/Sites-cartier-master/default/dw239464bc/images/large/624964b5d1fa5b4bb3477f89a0c4ac85.png?sw=350&sh=350&sm=fit&sfrm=png'),
