CREATE PROCEDURE [dbo].[Users_AsSelectList]
	@isEnabled bit
AS
	SELECT [value],[text] from User_List
	left join [User] on User_List.[value]=[User].Id
	where [User].[Enabled]=@isEnabled
	Order By User_List.[text]
RETURN 0
