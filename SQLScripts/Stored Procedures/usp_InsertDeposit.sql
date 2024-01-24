CREATE PROCEDURE usp_InsertDeposit
	@accNum int,
	@accType nvarchar(10),
	@pBalance money,
	@amt money,
	@newBal money
AS
BEGIN
	INSERT INTO tblDeposits(AccountNumber, AccountType, PreviousBalance, Amount, NewBalance)
	VALUES(@accNum, @accType, @pBalance, @amt, @newBal)
END
GO
