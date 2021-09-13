CREATE TABLE [dbo].[DepartmentUser]
(
	[UserId] INT NOT NULL , 
    [DepartmentId] INT NOT NULL,
	Primary Key (UserId,[DepartmentId]), 
    CONSTRAINT [FK_DepartmentUser_ToUser] FOREIGN KEY ([UserId]) REFERENCES [User]([Id]) on Delete cascade, 
    CONSTRAINT [FK_DepartmentUser_ToDepartment] FOREIGN KEY ([DepartmentId]) REFERENCES [Department]([Id]) on Delete cascade
)
