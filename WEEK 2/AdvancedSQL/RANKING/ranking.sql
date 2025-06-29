-- 🧹 1. Drop table if it already exists
IF OBJECT_ID('Products', 'U') IS NOT NULL
    DROP TABLE Products;

-- 🛠️ 2. Create the Products table
CREATE TABLE Products (
    ProductID INT PRIMARY KEY,
    ProductName VARCHAR(100),
    Category VARCHAR(50),
    Price DECIMAL(10, 2)
);

-- 📦 3. Insert sample product data
INSERT INTO Products (ProductID, ProductName, Category, Price) VALUES
(1, 'Laptop A', 'Electronics', 1000.00),
(2, 'Laptop B', 'Electronics', 900.00),
(3, 'Laptop C', 'Electronics', 1000.00),
(4, 'Shirt A', 'Clothing', 50.00),
(5, 'Shirt B', 'Clothing', 60.00),
(6, 'Shirt C', 'Clothing', 55.00),
(7, 'Book A', 'Books', 20.00),
(8, 'Book B', 'Books', 22.00),
(9, 'Book C', 'Books', 20.00);

-- 🔍 4. Query with ROW_NUMBER(), RANK(), DENSE_RANK()
SELECT *
FROM (
    SELECT 
        ProductID,
        ProductName,
        Category,
        Price,
        ROW_NUMBER() OVER (PARTITION BY Category ORDER BY Price DESC) AS RowNum,
        RANK()       OVER (PARTITION BY Category ORDER BY Price DESC) AS RankNum,
        DENSE_RANK() OVER (PARTITION BY Category ORDER BY Price DESC) AS DenseRankNum
    FROM Products
) ranked
WHERE RowNum <= 3 OR RankNum <= 3 OR DenseRankNum <= 3
ORDER BY Category, Price DESC;
