CREATE PROCEDURE usp_UpdateBalance
	@newBal money,
	@accNum int
AS
BEGIN
	UPDATE tblAccount SET InitialBalance = @newBal WHERE AccountID = @accNum
END