CREATE PROC [dbo].[uspMessageOptionIssueSelect] 
    @MessageOptionIssueID UNIQUEIDENTIFIER
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [MessageOptionIssueID], [MessageIssueID], [OptionIssueID], [Deleted], [DeletedBy], [DeletedOn] 
	FROM   [dbo].[MessageOptionIssue] 
	WHERE  ([MessageOptionIssueID] = @MessageOptionIssueID OR @MessageOptionIssueID IS NULL) 

	COMMIT