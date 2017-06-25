CREATE PROC [dbo].[uspOptionIssueDelete] 
    @OptionIssueID uniqueidentifier
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	UPDATE [dbo].[OptionIssue]
	SET 
		Deleted=1,
		DeletedOn=GETDATE()
	WHERE  [OptionIssueID] = @OptionIssueID
	COMMIT