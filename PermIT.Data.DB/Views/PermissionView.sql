CREATE VIEW [dbo].[Permission_List]
	AS SELECT Permission.Id 'Id',CONCAT([Application].[Name],', ',Permission.[Name]) 'text',Permission.[Name] 'Parent', Permission.Id 'ParentId', [Application].[Name],[Application].Id 'ApplicationId' FROM [Permission] Child
	left join [Application] on [Child].ApplicationId=[Application].Id
	left join [Permission]  on [Child].ParentId = [Permission].Id 
