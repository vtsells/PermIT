CREATE VIEW [dbo].[ChecklistTemplate_List]
	AS SELECT [ChecklistTemplate].Id 'value',[ChecklistTemplate].[Name] 'text', [Job].[Name] 'Job',[Job].Id 'JobId',[OnRemove] from [ChecklistTemplate]
	left join [Job] on [ChecklistTemplate].JobId=Job.Id
