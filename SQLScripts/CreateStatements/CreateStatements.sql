CREATE TABLE [tblLoginDetails] (
  [UserID] int IDENTITY (1,1),
  [Username] nvarchar(30) NOT NULL,
  [Password] nvarchar(16) NOT NULL,
  PRIMARY KEY ([UserID])
);


CREATE TABLE [tblAccount] (
  [AccountID] int IDENTITY (10000000,1) NOT NULL PRIMARY KEY,
  [Username] nvarchar(30) NOT NULL,
  [Firstname] nvarchar(20) NOT NULL,
  [Surname] nvarchar(20) NOT NULL,
  [Email] nvarchar(30) NOT NULL,
  [Phone] nvarchar(10) NOT NULL,
  [AddressLine1] nvarchar(50) NOT NULL,
  [AddressLine2] nvarchar(50) NOT NULL,
  [City] nvarchar(15) NOT NULL,
  [County] nvarchar(15) NOT NULL,
  [AccountType] nvarchar(15) NOT NULL,
  [SortCode] int NOT NULL,
  [InitialBalance] money NOT NULL,
  [OverdraftLimit] money NOT NULL,
);


CREATE TABLE [tblWithdraws] (
  [WithdrawalsID] int IDENTITY(1,1),
  [AccountNumber] int NOT NULL,
  [AccountType] nvarchar(10) NOT NULL,
  [Balance] money NOT NULL,
  [Amount] money NOT NULL,
  [NewBalance] money NOT NULL
  PRIMARY KEY ([WithdrawalsID])
);

CREATE TABLE [tblDeposits] (
  [DepositsID] int IDENTITY(1,1),
  [AccountNumber] int NOT NULL,
  [AccountType] nvarchar(10) NOT NULL,
  [PreviousBalance] money NOT NULL,
  [Amount] money NOT NULL,
  [NewBalance] money NOT NULL
  PRIMARY KEY ([DepositsID])
);

CREATE TABLE [tblTransfers] (
  [TransfersID] int IDENTITY(1,1),
  [AccountNumber] int NOT NULL,
  [AccountType] nvarchar(10) NOT NULL,
  [Balance] money NOT NULL,
  [ToAccountNumber] int NOT NULL,
  [ToAccountType] nvarchar(10) NOT NULL,
  [SortCode] int NOT NULL,
  [Amount] money NOT NULL,
  [ReferenceNumber] int NOT NULL,
  [DateTime] dateTime NOT NULL,
  PRIMARY KEY ([TransfersID])
);

CREATE TABLE tblTransactions(
	[TransactionID] int IDENTITY(1,1),
	[TransactionType] nvarchar(10) NOT NULL,
	[AccountNumber] int NOT NULL,
	[AccountType] nvarchar(10) NOT NULL,
	[PreviousBalance] money NOT NULL,
	[Amount] money NOT NULL,
	[NewBalance] money NOT NULL,
	[ToAccNum] int,
	[RefNum] int,
	[SortCode] int
)

INSERT INTO tblAccount(Username, Firstname, Surname, Email, Phone, AddressLine1, AddressLine2, City, County, AccountType, SortCode, InitialBalance, OverdraftLimit)
VALUES('Denise123', 'Santos', 'denise@dbs.ie', '1234545', 'North', 'Circular Road','Dublin', 'Dublin', 'Current', 101010, 450.00, 45.00)

INSERT INTO tblTransfers(AccountNumber, AccountType, Balance, ToAccountNumber, ToAccountType,SortCode,Amount, ReferenceNumber, [DateTime])
VALUES(10000000,'Current', 450.00, 100000001, 'Current', 101012, 45.00, 0, '2023-04-08 00:00:00')







