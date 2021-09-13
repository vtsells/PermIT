CREATE TABLE [dbo].[User]
(
	[Id]	INT IDENTITY (1, 1) NOT NULL Primary Key, 
    [FirstName] NVARCHAR(MAX) NULL, 
    [LastName] NVARCHAR(MAX) NULL, 
    [SID] NVARCHAR(MAX) NULL,
    [Enabled] bit

)
