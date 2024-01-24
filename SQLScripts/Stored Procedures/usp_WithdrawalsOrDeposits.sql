CREATE PROCEDURE usp_WithdrawalsOrDeposits
	@transType nvarchar(10)
AS
BEGIN
	SELECT TransactionID, AccountNumber, AccountType, PreviousBalance, Amount, NewBalance, TransactionType, RefNum
	FROM tblTransactions WHERE TransactionType = @transType
END
GO
