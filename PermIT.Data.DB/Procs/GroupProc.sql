CREATE PROCEDURE [dbo].[Group_AsSelectList]
	@applicationId int
AS
	SELECT * from Group_List
	where Group_List.ApplicationId=@applicationId
	Order By Group_List.[text]
RETURN 0
