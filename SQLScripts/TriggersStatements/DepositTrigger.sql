CREATE TRIGGER [DepositTrigger]
	ON [tblDeposits]
	FOR DELETE, INSERT, UPDATE
	AS
	BEGIN
	DECLARE @transType nvarchar(10)
	SET @transType = 'Deposit'

	DECLARE @accNum int
	SELECT @accNum = AccountNumber FROM tblDeposits

	DECLARE @accType nvarchar(10)
	SELECT @accType = AccountType FROM tblDeposits

	DECLARE @pBal money
	SELECT @pBal = PreviousBalance FROM tblDeposits

	DECLARE @amt money
	SELECT @amt = Amount FROM tblDeposits

	DECLARE @newBal money
	SELECT @newBal = NewBalance FROM tblDeposits

	DECLARE @toAcc int
	SET @toAcc = -1

	DECLARE @refNum int
	SELECT @refNum = DepositsID FROM tblDeposits

	DECLARE @sortCode int
	SET @sortCode = -1

	INSERT INTO tblTransactions(AccountNumber, AccountType, PreviousBalance, Amount,NewBalance,TransactionType,ToAccNum,RefNum,SortCode)
	VALUES(@accNum, @accType, @pBal, @amt, @newBal, @transType, @toAcc, @refNum, @sortCode)
	END