CREATE PROCEDURE [dbo].[ChecklistTemplateItem_AsSelectList]
	@checklistTemplateId int
AS
	SELECT * from ChecklistTemplateItem_List
	where ChecklistTemplateItem_List.ChecklistTemplateId=@checklistTemplateId
	Order By ChecklistTemplateItem_List.[text]
RETURN 0
