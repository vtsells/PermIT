CREATE PROCEDURE [dbo].[Department_AsSelectList]

AS
	SELECT * from Department_List
	Order By Department_List.[text]
RETURN 0
