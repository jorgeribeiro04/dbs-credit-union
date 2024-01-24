CREATE PROCEDURE usp_UpdateAccount
	@accNum int,
	@email nvarchar(30),
	@phone nvarchar(10),
	@add1 nvarchar(50),
	@add2 nvarchar(50),
	@city nvarchar(50),
	@cy nvarchar(15)
AS
BEGIN
	UPDATE tblAccount SET Email = @email, Phone = @phone, AddressLine1 = @add1, AddressLine2 = @add2, City = @city, County = @cy
	WHERE AccountID = @accNum
END
GO
