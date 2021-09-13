CREATE PROCEDURE [dbo].[ChecklistTemplate_AsSelectList]
	@jobId int,
	@onRemove bit
AS
	SELECT * from ChecklistTemplate_List
	where ChecklistTemplate_List.JobId=@jobId and ChecklistTemplate_List.OnRemove=@onRemove
	Order By ChecklistTemplate_List.[text]
RETURN 0
