CREATE PROCEDURE usp_Transfer
	@senderAccNum int,
	@accType nvarchar(10),
	@bal money,
	@toAccNum int,
	@toAccType nvarchar(10),
	@sortCode int,
	@amt money,
	@refNumber int,
	@date datetime
AS
BEGIN
	INSERT INTO tblTransfers(AccountNumber, AccountType, Balance, ToAccountNumber, ToAccountType, SortCode, Amount, ReferenceNumber, [DateTime])
	VALUES(@senderAccNum, @accType, @bal, @toAccNum, @toAccType, @sortCode, @amt, @refNumber, @date)
END
GO
