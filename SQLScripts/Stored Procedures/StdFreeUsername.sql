CREATE PROCEDURE stdFreeUsername
	@username nvarchar(30)
AS
BEGIN
	SELECT COUNT(*) FROM tblLoginDetails WHERE Username = @username
END
GO
