CREATE PROCEDURE usp_SelectMaxTransfer 
AS
BEGIN
	SELECT MAX(TransfersID) FROM tblTransfers
END
GO
