-- 1. Create Employees table (only if it doesn't exist)
IF NOT EXISTS (
    SELECT * FROM sysobjects WHERE name='Employees' AND xtype='U'
)
BEGIN
    CREATE TABLE Employees (
        EmployeeID INT IDENTITY(1,1) PRIMARY KEY,
        FirstName VARCHAR(50),
        LastName VARCHAR(50),
        DepartmentID INT,
        Salary DECIMAL(10,2),
        JoinDate DATE
    );
END;
GO

-- 2. Drop existing procedures if they exist
IF OBJECT_ID('sp_InsertEmployee', 'P') IS NOT NULL
    DROP PROCEDURE sp_InsertEmployee;
GO

IF OBJECT_ID('sp_GetEmpByDepart', 'P') IS NOT NULL
    DROP PROCEDURE sp_GetEmpByDepart;
GO

-- 3. Create Insert Procedure
CREATE PROCEDURE sp_InsertEmployee
    @FirstName VARCHAR(50),
    @LastName VARCHAR(50),
    @DepartmentID INT,
    @Salary DECIMAL(10,2),
    @JoinDate DATE
AS
BEGIN
    -- Insert only if this exact employee doesn't already exist
    IF NOT EXISTS (
        SELECT 1 FROM Employees
        WHERE FirstName = @FirstName AND LastName = @LastName
              AND DepartmentID = @DepartmentID AND JoinDate = @JoinDate
    )
    BEGIN
        INSERT INTO Employees (FirstName, LastName, DepartmentID, Salary, JoinDate)
        VALUES (@FirstName, @LastName, @DepartmentID, @Salary, @JoinDate);
    END
END;
GO

-- 4. Create Select Procedure
CREATE PROCEDURE sp_GetEmpByDepart
    @DepartmentID INT
AS
BEGIN
    SELECT FirstName, LastName, DepartmentID, Salary, JoinDate
    FROM Employees
    WHERE DepartmentID = @DepartmentID;
END;
GO

-- 5. Clean up duplicate test data (only needed once)
DELETE FROM Employees
WHERE FirstName = 'Amit' AND LastName = 'Sharma' AND DepartmentID = 2;
GO

-- 6. Test Insert (only once)
EXEC sp_InsertEmployee 
    @FirstName = 'Amit',
    @LastName = 'Sharma',
    @DepartmentID = 2,
    @Salary = 48000.00,
    @JoinDate = '2025-06-28';
GO

-- 7. Test Select
EXEC sp_GetEmpByDepart 
    @DepartmentID = 2;
GO
