
CREATE PROC [dbo].[uspIssueDelete] 
    @IssueID uniqueidentifier
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN
	UPDATE [dbo].[Issue] 
	SET 
		Deleted=1,
		DeletedOn=GETDATE()
	WHERE  [IssueID] = @IssueID


	UPDATE [dbo].[OptionIssue] 
	SET 
		Deleted=1,
		DeletedOn=GETDATE()
	WHERE  [IssueID] = @IssueID


	UPDATE [dbo].[LaterRatedIssue] 
	SET 
		Deleted=1,
		DeletedOn=GETDATE()
	WHERE  [IssueID] = @IssueID


	UPDATE [dbo].[NumericIssue] 
	SET 
		Deleted=1,
		DeletedOn=GETDATE()
	WHERE  [IssueID] = @IssueID
	COMMIT