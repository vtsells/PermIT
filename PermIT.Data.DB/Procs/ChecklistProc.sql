CREATE PROCEDURE [dbo].[Checklist_AsSelectList]

AS
	SELECT * from Checklist_List
	Order By Checklist_List.[text]
RETURN 0
