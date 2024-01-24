CREATE PROCEDURE usp_InsertWithdraw
	@accNum int,
	@acType nvarchar(10),
	@bal money,
	@amt money,
	@newBal money
AS
BEGIN
	INSERT INTO tblWithdraws(AccountNumber, AccountType, Balance, Amount, NewBalance)
	VALUES(@accNum, @acType,@bal,@amt,@newBal)
END
GO
