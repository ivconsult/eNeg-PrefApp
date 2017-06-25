
CREATE PROC [dbo].[uspNumericIssueDelete] 
    @NumericIssueID uniqueidentifier
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	UPDATE [dbo].[NumericIssue] 
	SET 
		Deleted=1,
		DeletedOn=GETDATE()
	WHERE  [NumericIssueID] = @NumericIssueID 
	COMMIT