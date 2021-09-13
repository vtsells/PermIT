CREATE PROCEDURE [dbo].[ChecklistItem_AsSelectList]
	@checklistId int
AS
	SELECT * from ChecklistItem_List
	where ChecklistItem_List.ChecklistId=@checklistId
	Order By ChecklistItem_List.[text]
RETURN 0
