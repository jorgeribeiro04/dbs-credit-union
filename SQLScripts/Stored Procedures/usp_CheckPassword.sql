CREATE PROCEDURE usp_CheckPassword
	@user nvarchar(30),
	@pass nvarchar(16)
AS
BEGIN
	SET NOCOUNT ON;
	IF EXISTS(SELECT * FROM tblLoginDetails WHERE Username = @user AND [Password] = @pass)
		SELECT 'true' AS UserExists 
	ELSE 
		SELECT 'false' AS UserExists
END
GO
