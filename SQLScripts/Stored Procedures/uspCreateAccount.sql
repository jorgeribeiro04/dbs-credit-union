CREATE PROCEDURE uspCreateAccount
	@username nvarchar(30),
	@fn nvarchar(20),
	@sn nvarchar(20),
	@email nvarchar(30),
	@phone nvarchar(10),
	@add1 nvarchar(50),
	@add2 nvarchar(50),
	@city nvarchar(15),
	@county nvarchar(15),
	@accType nvarchar(15),
	@sortCode int,
	@bal money,
	@overdraft money
AS
BEGIN
	INSERT INTO tblAccount(Username, Firstname, Surname, Email, Phone, AddressLine1, AddressLine2, City, County, AccountType, SortCode, InitialBalance, OverdraftLimit)
	VALUES(@username, @fn, @sn, @email, @phone, @add1, @add2, @city, @county, @accType, @sortCode, @bal, @overdraft)
END
GO
