CREATE PROCEDURE [dbo].[Job_AsSelectList]

AS
	SELECT * from Job_List
	Order By Job_List.[text]
RETURN 0
