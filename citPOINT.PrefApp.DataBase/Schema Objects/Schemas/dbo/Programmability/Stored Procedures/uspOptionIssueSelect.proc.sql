CREATE PROC [dbo].[uspOptionIssueSelect] 
    @OptionIssueID UNIQUEIDENTIFIER
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [OptionIssueID], [OptionIssueValue], [IssueID], [OptionIssueWeight], [Deleted], [DeletedBy], [DeletedOn] 
	FROM   [dbo].[OptionIssue] 
	WHERE  ([OptionIssueID] = @OptionIssueID OR @OptionIssueID IS NULL) 

	COMMIT