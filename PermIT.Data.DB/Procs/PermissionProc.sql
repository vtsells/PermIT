CREATE PROCEDURE [dbo].[Permission_AsSelectList]
	@applicationId int = null,
	@parentId int = null
AS
	SELECT * from Permission_List
		where (@parentId is null or Permission_List.ParentId=@parentId) and (@applicationId=Permission_List.ApplicationId)
	Order By Permission_List.[text]

RETURN 0
