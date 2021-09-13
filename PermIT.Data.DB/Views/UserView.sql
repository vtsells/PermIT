CREATE VIEW [dbo].[User_List]
	AS SELECT Id 'value',CONCAT(FirstName,' ',LastName) 'text' FROM [User]
go
CREATE VIEW [dbo].[User_IncludeJobs]
as
select [User].Id,FirstName,LastName,[Enabled],Jobs from [User]
left join
(select [User].Id 'userId',STRING_AGG(Job.[Name],', ') WITHIN GROUP (ORDER BY Job.[Name] ASC) 'Jobs' from [User]
left join JobUser on [User].Id=JobUser.UserId
left join Job on JobUser.JobId=Job.Id
group by [User].Id) jt on [User].Id=jt.userId 

go
