CREATE PROCEDURE uspMyAccountDetails
	@accNum int
AS
BEGIN
	SELECT * FROM tblAccount WHERE AccountID = @accNum
END
GO
