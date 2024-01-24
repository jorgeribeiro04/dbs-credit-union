CREATE TRIGGER [WithdrawTrigger]
	ON [tblWithdraws]
	FOR DELETE, INSERT, UPDATE
	AS
	BEGIN
	DECLARE @transType nvarchar(10)
	SET @transType = 'Withdraw'

	DECLARE @accNum int
	SELECT @accNum = AccountNumber FROM tblWithdraws

	DECLARE @accType nvarchar(10)
	SELECT @accType = AccountType FROM tblWithdraws

	DECLARE @pBal money
	SELECT @pBal = Balance FROM tblWithdraws

	DECLARE @amt money
	SELECT @amt = Amount FROM tblWithdraws

	DECLARE @newBal money
	SELECT @newBal = NewBalance FROM tblWithdraws

	DECLARE @toAcc int
	SET @toAcc = -1

	DECLARE @refNum int
	SELECT @refNum = WithdrawalsID FROM tblWithdraws

	DECLARE @sortCode int
	SET @sortCode = -1

	INSERT INTO tblTransactions(AccountNumber, AccountType, PreviousBalance, Amount,NewBalance,TransactionType,ToAccNum,RefNum,SortCode)
	VALUES(@accNum, @accType, @pBal, @amt, @newBal, @transType, @toAcc, @refNum, @sortCode)
	END