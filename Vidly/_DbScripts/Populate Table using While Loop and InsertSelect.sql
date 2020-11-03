-- >> Author: Harbin Ramo
-- >> Date: 2020-11-03 10:08AM

-- 1. (Optional) Clear any data from <TableName> Table
--TRUNCATE TABLE <TableName>

-- 2. While Loop and InsertSelect
DECLARE @MinValue INT
DECLARE @MaxValue INT
SET @MinValue = 1
SET @MaxValue = 100
WHILE @MinValue <= @MaxValue
	BEGIN
		INSERT INTO Customers (Name, BirthDate, IsSubscribedToNewsLetter, MembershipTypeId) SELECT 'Harbin_'+CONVERT(NVARCHAR,@MinValue),'2020-06-26',0,1
		SET @MinValue = @MinValue + 1
	END
	
