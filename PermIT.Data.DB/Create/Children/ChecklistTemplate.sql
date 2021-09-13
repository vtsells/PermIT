CREATE TABLE [dbo].[ChecklistTemplate]
(
	[Id]	INT IDENTITY (1, 1) NOT NULL Primary Key, 
    [Name]NVARCHAR(MAX) NULL, 
    [JobId] INT NULL,
    [OnRemove] BIT NULL, 
    CONSTRAINT [Job_ChecklistTemplate] FOREIGN KEY ([JobId]) REFERENCES [dbo].[Job] ([Id]) on delete cascade

)
