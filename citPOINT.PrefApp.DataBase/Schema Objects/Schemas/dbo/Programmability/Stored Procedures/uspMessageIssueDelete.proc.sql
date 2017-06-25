CREATE PROC [dbo].[uspMessageIssueDelete] 
    @MessageIssueID uniqueidentifier
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

		UPDATE [dbo].[MessageIssue]  
		SET 
			Deleted=1,
			DeletedOn=GETDATE()
		WHERE  [MessageIssueID] = @MessageIssueID 


	COMMIT