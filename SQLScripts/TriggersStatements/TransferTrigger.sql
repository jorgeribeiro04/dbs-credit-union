CREATE TRIGGER [TransferTrigger]
	ON [tblTransfers]
	FOR DELETE, INSERT, UPDATE
	AS
	BEGIN
	DECLARE @transType nvarchar(10)
	SET @transType = 'Transfer'

	DECLARE @accNum int
	SELECT @accNum = AccountNumber FROM tblTransfers

	DECLARE @accType nvarchar(10)
	SELECT @accType = AccountType FROM tblTransfers

	DECLARE @pBal money
	SELECT @pBal = Balance FROM tblTransfers

	DECLARE @amt money
	SELECT @amt = Amount FROM tblTransfers

	DECLARE @newBal money
	SET @newBal = @pBal - @amt

	DECLARE @toAcc int
	SELECT @toAcc = ToAccountNumber FROM tblTransfers

	DECLARE @refNum int
	SELECT @refNum = ReferenceNumber FROM tblTransfers

	DECLARE @sortCode int
	SELECT @sortCode = SortCode FROM tblTransfers

	INSERT INTO tblTransactions(AccountNumber, AccountType, PreviousBalance, Amount,NewBalance,TransactionType,ToAccNum,RefNum,SortCode)
	VALUES(@accNum, @accType, @pBal, @amt, @newBal, @transType, @toAcc, @refNum, @sortCode)
	END