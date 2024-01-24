CREATE PROCEDURE stdInsertLoginDetails
	@username nvarchar(30),
	@password nvarchar(16)
AS
BEGIN TRAN 
	INSERT INTO tblLoginDetails(Username, [Password])
	VALUES(@username, @password)

	IF @@ERROR <> 0 OR @@ROWCOUNT <> 1
	BEGIN
	PRINT 'ERROR! NO INSERT HAS BEEN DONE!'
	SELECT ERROR_MESSAGE()
	ROLLBACK TRAN
	RETURN -1
	END
COMMIT TRAN
GO

