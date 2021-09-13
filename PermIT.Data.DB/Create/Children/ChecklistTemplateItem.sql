CREATE TABLE [dbo].[ChecklistTemplateItem]
(
	[Id]	INT IDENTITY (1, 1) NOT NULL Primary Key, 
    [Name]NVARCHAR(MAX) NULL, 
    [ChecklistTemplateId] INT NULL,
    CONSTRAINT [ChecklistTemplate_ChecklistTemplateItem] FOREIGN KEY ([ChecklistTemplateId]) REFERENCES [dbo].[ChecklistTemplate] ([Id]) on delete cascade

)
