CREATE PROC [dbo].[uspMessageLaterRatedIssueSelect] 
    @MessageLaterRatedIssueID UNIQUEIDENTIFIER
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [MessageLaterRatedIssueID], [MessageIssueID], [LaterRatedIssueID], [Deleted], [DeletedBy], [DeletedOn] 
	FROM   [dbo].[MessageLaterRatedIssue] 
	WHERE  ([MessageLaterRatedIssueID] = @MessageLaterRatedIssueID OR @MessageLaterRatedIssueID IS NULL) 

	COMMIT