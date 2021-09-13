CREATE VIEW [dbo].[Group_List]
	AS SELECT [Group].Id 'value',[Group].[Name] 'text',[Application].[Name] 'Application',[Application].Id 'ApplicationId' from [Group]
	left join [Application] on [Group].ApplicationId=[Application].Id
