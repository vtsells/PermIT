CREATE VIEW [dbo].[Checklist_List]
	AS SELECT Id 'value',[Name] 'text' FROM [Checklist]
	go
create view [dbo].[ChecklistPerUser]
as select [Checklist].Id,[Name],[User].Id 'userId',Concat(FirstName,', ',LastName) 'user',IsComplete from [Checklist]
left join [User] on Checklist.UserId=[User].Id