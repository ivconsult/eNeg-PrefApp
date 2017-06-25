CREATE PROCEDURE [dbo].[uspLaterRatedIssueSelect]
	 @LaterRatedIssueID UNIQUEIDENTIFIER
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [LaterRatedIssueID], [LaterRatedIssueValue], [IssueID], [LaterRatedIssueWeight], [Deleted], [DeletedBy], [DeletedOn]
	FROM   [dbo].[LaterRatedIssue] 
	WHERE  ([LaterRatedIssueID] = @LaterRatedIssueID OR @LaterRatedIssueID IS NULL) 

	COMMIT