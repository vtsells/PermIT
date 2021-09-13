CREATE TABLE [dbo].[Checklist]
(
	[Id]	INT IDENTITY (1, 1) NOT NULL Primary Key, 
    [Name] NVARCHAR(MAX) NULL, 
    [UserId] INT NULL,
    [IsComplete] INT NULL DEFAULT 0, 
    CONSTRAINT [Checklist_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id]) on delete cascade

)
