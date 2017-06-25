CREATE PROC [dbo].[uspMessageLaterRatedIssueDelete] 
    @MessageLaterRatedIssueID uniqueidentifier
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo] .MessageLaterRatedIssue 
	SET   Deleted = 1, DeletedOn = GETDATE()
	WHERE  [MessageLaterRatedIssueID] = @MessageLaterRatedIssueID

	COMMIT