CREATE TABLE [dbo].[GroupUser]
(
	[UserId] INT NOT NULL , 
    [GroupId] INT NOT NULL,
	Primary Key (UserId,GroupId), 
    CONSTRAINT [FK_GroupUser_ToUser] FOREIGN KEY ([UserId]) REFERENCES [User]([Id]) on Delete cascade, 
    CONSTRAINT [FK_GroupUser_ToGroup] FOREIGN KEY ([GroupId]) REFERENCES [Group]([Id]) on Delete cascade
)
