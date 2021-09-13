CREATE PROCEDURE [dbo].[Application_AsSelectList]

AS
	SELECT * from Application_List
	Order By Application_List.[text]
RETURN 0
