CREATE TABLE [dbo].[ApplicationUser]
(
	[UserId] INT NOT NULL , 
    [ApplicationId] INT NOT NULL,
	Primary Key (UserId,[ApplicationId]), 
    CONSTRAINT [FK_ApplicationUser_ToUser] FOREIGN KEY ([UserId]) REFERENCES [User]([Id]) on Delete cascade, 
    CONSTRAINT [FK_ApplicationUser_ToApplication] FOREIGN KEY ([ApplicationId]) REFERENCES [Application]([Id]) on Delete cascade
)
