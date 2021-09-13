CREATE TABLE [dbo].[Permission]
(
	[Id]	INT IDENTITY (1, 1) NOT NULL Primary Key, 
    [Name]NVARCHAR(MAX) NULL, 
    [ApplicationId] INT NULL,
     [ParentId] INT NULL, 
    CONSTRAINT [Application_Permission] FOREIGN KEY ([ApplicationId]) REFERENCES [dbo].[Application] ([Id]) on delete cascade,
    CONSTRAINT [Permission_Permission] FOREIGN KEY ([ParentId]) REFERENCES [dbo].[Permission] ([Id]),

)
