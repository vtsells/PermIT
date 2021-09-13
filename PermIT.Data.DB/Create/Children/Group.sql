CREATE TABLE [dbo].[Group]
(
	[Id]	INT IDENTITY (1, 1) NOT NULL Primary Key, 
    [Name]NVARCHAR(MAX) NULL, 
    [ApplicationId] INT NULL,
     CONSTRAINT [Application_Group] FOREIGN KEY ([ApplicationId]) REFERENCES [dbo].[Application] ([Id]) on delete cascade

)
