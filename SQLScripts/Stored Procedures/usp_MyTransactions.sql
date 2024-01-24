CREATE PROCEDURE usp_MyTransactions
	@accNum int
AS
BEGIN
	SELECT * FROM tblTransactions WHERE AccountNumber = @accNum
END
GO
