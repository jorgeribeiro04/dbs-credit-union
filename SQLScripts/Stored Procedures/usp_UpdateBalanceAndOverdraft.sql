CREATE PROCEDURE usp_UpdateBalanceAndOverdraft
	@newBal money,
	@newOverdraft money,
	@accNum int
AS
BEGIN
	UPDATE tblAccount SET InitialBalance = @newBal, OverdraftLimit = @newOverdraft WHERE AccountID = @accNum
END
GO
