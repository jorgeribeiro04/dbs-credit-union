CREATE PROCEDURE usp_SelectAccountNum
	@username nvarchar(30)
AS
BEGIN
	SELECT AccountID FROM tblAccount WHERE Username = @username
END
GO
