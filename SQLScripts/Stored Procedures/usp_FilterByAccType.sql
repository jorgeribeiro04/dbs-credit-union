CREATE PROCEDURE usp_FilterByAccType
	@accType nvarchar(10)
AS
BEGIN
	SELECT * FROM tblTransactions WHERE AccountType = @accType
END
GO
