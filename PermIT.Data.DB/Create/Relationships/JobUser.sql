CREATE TABLE [dbo].[JobUser]
(
	[UserId] INT NOT NULL , 
    [JobId] INT NOT NULL,
	Primary Key (UserId,[JobId]), 
    CONSTRAINT [FK_JobUser_ToUser] FOREIGN KEY ([UserId]) REFERENCES [User]([Id]) on Delete cascade, 
    CONSTRAINT [FK_JobUser_ToJob] FOREIGN KEY ([JobId]) REFERENCES [Job]([Id]) on Delete cascade
)
