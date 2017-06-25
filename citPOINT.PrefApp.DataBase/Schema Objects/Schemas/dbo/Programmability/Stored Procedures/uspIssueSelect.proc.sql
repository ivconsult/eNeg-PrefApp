CREATE PROC [dbo].[uspIssueSelect] 
    @IssueID UNIQUEIDENTIFIER
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [IssueID], [IssueName], [PreferenceSetID], [IssueTypeID], [IssueWeight], [Deleted], [DeletedBy], [DeletedOn] 
	FROM   [dbo].[Issue] 
	WHERE  ([IssueID] = @IssueID OR @IssueID IS NULL) 

	COMMIT