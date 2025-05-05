--Server: .\SQLEXPRESS
--Database: ESMDS

CREATE DATABASE ESMDS
GO
USE ESMDS
GO

CREATE TABLE Employee (
    ID VARCHAR(10) PRIMARY KEY,
    [Name] NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    [Password] NVARCHAR(100) NOT NULL,
    [Street Address] NVARCHAR(255) NOT NULL,
    Ward NVARCHAR(100) NOT NULL,
    District NVARCHAR(100) NOT NULL,
    City NVARCHAR(100) NOT NULL,
    Phone CHAR(10) NOT NULL UNIQUE,
    Salary INT NOT NULL,
    [Role] NVARCHAR(10) NOT NULL CHECK ([Role] IN ('Staff', 'Manager')),
    [Date Employed] DATETIME NOT NULL DEFAULT GETDATE()
);
GO

CREATE TABLE Customer (
    ID VARCHAR(10) PRIMARY KEY,
    [Name] NVARCHAR(100) NOT NULL,
    Phone VARCHAR(15) NOT NULL
);
GO

CREATE TABLE Product (
    ID VARCHAR(10) PRIMARY KEY,
    [Name] NVARCHAR(100) NOT NULL,
    Price INT NOT NULL,
    Quantity INT NOT NULL,
    [Stock Entry Quantity] INT NOT NULL,
    [Stock Entry Date] DATETIME NOT NULL,
    [Sold Out Date] DATETIME NULL -- sẽ được cập nhật sau
);
GO

CREATE TABLE Voucher (
    ID VARCHAR(10) PRIMARY KEY,
    [Description] NVARCHAR(255),
	[Minimum Order Price] INT NOT NULL DEFAULT 0,
    [Discount Amount] INT NOT NULL, -- Số tiền giảm trực tiếp (VND) hoặc %
	[Discount Maximum Amount] INT NOT NULL,
    [Start Date] DATETIME NOT NULL,
    [End Date] DATETIME NOT NULL,
    [Is Active] BIT NOT NULL DEFAULT 1
);
GO

CREATE TABLE Invoice (
    ID VARCHAR(30) PRIMARY KEY,
    CustomerID VARCHAR(10) NOT NULL,
    EmployeeID VARCHAR(10) NOT NULL,
	VoucherID VARCHAR(10) NULL,
	[Invoice Type] CHAR(1) NOT NULL CHECK ([Invoice Type] IN ('S', 'R', 'W')), -- Sale, Return, Warranty
    [Total Amount] INT NOT NULL,
    [Create Date] DATETIME NOT NULL,
    FOREIGN KEY (CustomerID) REFERENCES Customer(ID),
    FOREIGN KEY (EmployeeID) REFERENCES Employee(ID),
    FOREIGN KEY (VoucherID) REFERENCES Voucher(ID)
);
GO

CREATE TABLE InvoiceLine (
    ID INT NOT NULL,
    InvoiceID VARCHAR(30) NOT NULL,
    ProductID VARCHAR(10) NOT NULL,
    Quantity INT NOT NULL,
    [Unit Price] INT NOT NULL,
    [Total Price] AS (Quantity * [Unit Price]) PERSISTED,
    FOREIGN KEY (InvoiceID) REFERENCES Invoice(ID),
    FOREIGN KEY (ProductID) REFERENCES Product(ID),
    PRIMARY KEY (ID, InvoiceID) -- Đảm bảo duy nhất theo InvoiceID
);
GO

CREATE TRIGGER InvoiceLine_Insert
ON InvoiceLine
AFTER INSERT
AS
BEGIN
    -- Cập nhật lại số thứ tự ID cho từng InvoiceID
    ;WITH InvoiceLineNumbered AS (
        SELECT 
            ROW_NUMBER() OVER (PARTITION BY InvoiceID ORDER BY ID) AS RowNum, -- Tạo số thứ tự cho từng InvoiceID
            ID, InvoiceID
        FROM InvoiceLine
        WHERE InvoiceID IN (SELECT InvoiceID FROM inserted)  -- Chỉ xét các InvoiceID vừa được chèn
    )
    UPDATE IL
    SET IL.ID = ILN.RowNum
    FROM InvoiceLine IL
    INNER JOIN InvoiceLineNumbered ILN
        ON IL.ID = ILN.ID;
END
GO

CREATE TRIGGER DecreaseProductQuantity_OnInvoiceLineInsert
ON InvoiceLine
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    -- Giảm số lượng tồn kho trong bảng Product
    UPDATE P
    SET P.Quantity = P.Quantity - IL.Quantity
    FROM Product P
    INNER JOIN inserted IL ON P.ID = IL.ProductID;

	-- Cập nhật ngày hết hàng nếu số lượng bằng 0 và chưa có SoldOutDate
    UPDATE P
    SET P.[Sold Out Date] = GETDATE()
    FROM Product P
    INNER JOIN inserted IL ON P.ID = IL.ProductID
    WHERE P.Quantity = 0
END
GO

CREATE TRIGGER UpdateProduct_StockEntry
ON Product
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE P
    SET 
        P.[Stock Entry Date] = GETDATE(),
        P.[Sold Out Date] = NULL
    FROM Product P
    INNER JOIN inserted i ON P.ID = i.ID
    INNER JOIN deleted d ON d.ID = i.ID
    WHERE 
        d.Quantity = 0 AND       -- Trước đó số lượng là 0
        i.Quantity > 0;          -- Bây giờ số lượng > 0
END;
GO


-- PASSWORD FOR SAMPLE ACCOUNT ADMIN AND STAFF: example123
INSERT INTO Employee (ID, [Name], Email, [Password], [Street Address], Ward, District, City, Phone, Salary, [Role])
VALUES
('M0016201', N'Admin', 'admin@example.com', '6165d066d7dca67b64005d450b0d40ea06f58434084c54134d2e77fe190e47a7', '123 79 St.', N'Tân Quy', N'7', N'Hồ Chí Minh', '0907586201', 5000, 'Manager'),
('S0026202', N'Staff', 'staff@example.com', '6165d066d7dca67b64005d450b0d40ea06f58434084c54134d2e77fe190e47a7', '123 79 St.', N'Tân Quy', N'7', N'Hồ Chí Minh', '0907586202', 5000, 'Staff')

-- PASSWORD FOR THESE ACCOUNT: q-tran14
INSERT INTO Employee (ID, [Name], Email, [Password], [Street Address], Ward, District, City, Phone, Salary, [Role])
VALUES 
('S0033474', N'Trần Quang Quân', 'moly098.tq@gmail.com', '3535f4fc83f6c7e8b75333725108a4834ec341a67ed6d6b92466848cdf86b211', '123 79 St.', N'Tân Quy', N'7', N'Hồ Chí Minh', '0773233474', 2000, 'Staff'),
('M0043473', N'Trần Thành Quang', 'tranquangquan.it@gmail.com', '3535f4fc83f6c7e8b75333725108a4834ec341a67ed6d6b92466848cdf86b211', '123 79 St.', N'Tân Quy', N'7', N'Hồ Chí Minh', '0773233473', 2000, 'Manager')

INSERT INTO Product (ID, [Name], Price, Quantity, [Stock Entry Quantity], [Stock Entry Date], [Sold Out Date])
VALUES 
('P001', N'Kính Mát Classic', 800000, 10, 10, '2025-01-05', NULL),
('P002', N'Kính Cận Gọng Tròn', 600000, 5, 10, '2025-02-12', NULL),
('P003', N'Kính Râm Polarized', 1200000, 8, 10, '2025-01-20', NULL),
('P004', N'Gọng Kính Titanium', 1500000, 2, 5, '2025-03-01', NULL),
('P005', N'Kính Chống Ánh Sáng Xanh', 950000, 4, 5, '2025-03-15', NULL),
('P006', N'Kính Thời Trang Nam', 700000, 6, 10, '2025-01-25', NULL),
('P007', N'Kính Cận Trẻ Em', 550000, 3, 5, '2025-03-20', NULL),
('P008', N'Gọng Nhựa Dẻo', 500000, 7, 10, '2025-02-28', NULL),
('P009', N'Kính Đổi Màu', 1100000, 5, 5, '2025-01-10', NULL),
('P010', N'Kính Mắt Vuông', 650000, 9, 10, '2025-02-05', NULL),
('P011', N'Kính Mát Retro', 870000, 4, 5, '2025-01-15', NULL),
('P012', N'Kính Tròn Kim Loại', 990000, 2, 5, '2025-02-10', NULL),
('P013', N'Kính Gọng Vàng', 1300000, 1, 3, '2025-03-05', NULL),
('P014', N'Kính Học Sinh', 480000, 6, 10, '2025-03-18', NULL),
('P015', N'Gọng Cận Siêu Nhẹ', 740000, 5, 7, '2025-02-22', NULL);

INSERT INTO Customer (ID, [Name], Phone)
VALUES 
('C001', N'Lê Thị B', '0912345678'),
('C002', N'Trần Văn C', '0923456789'),
('C003', N'Phạm Thị D', '0934567890');

INSERT INTO Voucher (ID, [Description], [Minimum Order Price], [Discount Amount], [Discount Maximum Amount], [Start Date], [End Date])
VALUES 
('V001', N'Giảm 50K cho đơn hàng từ 500K',500000, 50000, 50000, '2025-05-01', '2025-05-31'),
('V002', N'Giảm 100K cho đơn hàng từ 1 triệu',1000000, 100000, 100000, '2025-05-01', '2025-05-31'),
('V003', N'Giảm 20K cho đơn hàng bất kỳ',0, 20000, 20000, '2025-05-01', '2025-06-30'),
('V004', N'Voucher sinh nhật: giảm 150K',0, 150000, 150000, '2025-05-01', '2025-05-07'),
('V005', N'Giảm 15% khi mua lần đầu',0, 15, 70000, '2025-04-01', '2025-06-01');
INSERT INTO Voucher (ID, [Description], [Minimum Order Price], [Discount Amount], [Discount Maximum Amount], [Start Date], [End Date])
VALUES 
('V006', N'Giảm 10% (tối đa 25k) cho đơn hàng từ 100K',100000, 10, 25000, '2025-05-01', '2025-05-31')

--INSERT INTO Invoice (ID, CustomerID, EmployeeID, VoucherID, [Invoice Type], [Total Amount], [Create Date])
--VALUES 
--('S310320250001S2S003', 'C002', 'S003', 'V001', 'S', 3350000, GETDATE());

--INSERT INTO InvoiceLine (ID, InvoiceID, ProductID, Quantity, [Unit Price])
--VALUES
--(1, 'S310320250001S2S003', 'P001', 2, 800000),  -- Product P001, Quantity = 2, UnitPrice = 800000
--(2, 'S310320250001S2S003', 'P002', 1, 600000),  -- Product P002, Quantity = 1, UnitPrice = 600000
--(3, 'S310320250001S2S003', 'P003', 1, 1200000);  -- Product P003, Quantity = 1, UnitPrice = 1200000

SELECT * FROM Employee
SELECT * FROM Customer
SELECT * FROM Product
SELECT * FROM Voucher
SELECT * FROM Invoice
SELECT * FROM InvoiceLine
