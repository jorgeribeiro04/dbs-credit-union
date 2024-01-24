CREATE PROCEDURE uspMyAccount
	@username nvarchar(30)
AS
BEGIN
	SELECT * FROM tblAccount WHERE Username = @username
END