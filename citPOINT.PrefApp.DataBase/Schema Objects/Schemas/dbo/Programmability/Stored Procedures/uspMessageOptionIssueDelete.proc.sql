CREATE PROC [dbo].[uspMessageOptionIssueDelete] 
    @MessageOptionIssueID uniqueidentifier
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	update [dbo] .MessageOptionIssue 
	set Deleted =1, DeletedOn = GETDATE()
	WHERE  [MessageOptionIssueID] = @MessageOptionIssueID

	COMMIT