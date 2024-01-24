CREATE PROCEDURE usp_ValidLogin
	@user nvarchar(30),
	@pass nvarchar (16)
AS
BEGIN
	SELECT COUNT(*) FROM tblLoginDetails WHERE [Username] = @user and [Password] = @pass
END
GO
