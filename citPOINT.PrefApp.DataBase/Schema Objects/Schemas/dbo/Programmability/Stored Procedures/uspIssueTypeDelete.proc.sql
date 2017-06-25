
CREATE PROC [dbo].[uspIssueTypeDelete] 
    @IssueTypeID uniqueidentifier
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[IssueType]
	WHERE  [IssueTypeID] = @IssueTypeID

	COMMIT