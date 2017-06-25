CREATE PROC [dbo].[uspLaterRatedIssueDelete] 
    @LaterRatedIssueID uniqueidentifier
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	UPDATE [dbo].[LaterRatedIssue] 
	SET 
		Deleted=1,
		DeletedOn=GETDATE()
	WHERE  [LaterRatedIssueID] = @LaterRatedIssueID
	COMMIT