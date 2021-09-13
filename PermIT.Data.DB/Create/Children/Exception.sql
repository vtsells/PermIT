CREATE TABLE [dbo].[Exception]
(
	[Id]	INT IDENTITY (1, 1) NOT NULL Primary Key, 
    [Reason]VARCHAR(MAX) NULL, 
    [When] DATETIME NULL, 
    [UserId] INT NULL, 
    [PermissionId] INT NULL,
     CONSTRAINT [User_Exception] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id]) on delete cascade,
     CONSTRAINT [Permission_Exception] FOREIGN KEY ([PermissionId]) REFERENCES [dbo].[Permission] ([Id]) on delete cascade

)
