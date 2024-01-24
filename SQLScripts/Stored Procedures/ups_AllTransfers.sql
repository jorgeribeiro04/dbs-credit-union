CREATE PROCEDURE usp_AllTransfer
	@transType nvarchar(10)
AS
BEGIN

	SELECT * FROM tblTransactions WHERE TransactionType = @transType
END
GO
