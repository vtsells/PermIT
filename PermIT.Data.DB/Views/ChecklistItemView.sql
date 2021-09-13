CREATE VIEW [dbo].[ChecklistItem_List]
	AS SELECT [ChecklistItem].Id 'value',[ChecklistItem].[Name] 'text', [Checklist].[Name] 'Checklist',[Checklist].Id 'ChecklistId' from [ChecklistItem]
	left join [Checklist] on [ChecklistItem].ChecklistId=Checklist.Id
