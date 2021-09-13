CREATE VIEW [dbo].[ChecklistTemplateItem_List]
	AS SELECT [ChecklistTemplateItem].Id 'value',[ChecklistTemplateItem].[Name] 'text', [ChecklistTemplate].[Name] 'ChecklistTemplate',[ChecklistTemplate].Id 'ChecklistTemplateId' from [ChecklistTemplateItem]
	left join [ChecklistTemplate] on [ChecklistTemplateItem].ChecklistTemplateId=ChecklistTemplate.Id
