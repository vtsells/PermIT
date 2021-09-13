CREATE TABLE [dbo].[ChecklistItem]
(
	[Id]	INT IDENTITY (1, 1) NOT NULL Primary Key, 
    [Name]NVARCHAR(MAX) NULL, 
    [ChecklistId] INT NULL,
    [Completed] BIT NOT NULL DEFAULT 0, 
    CONSTRAINT [Checklist_ChecklistItem] FOREIGN KEY ([ChecklistId]) REFERENCES [dbo].[Checklist] ([Id]) on delete cascade

)
