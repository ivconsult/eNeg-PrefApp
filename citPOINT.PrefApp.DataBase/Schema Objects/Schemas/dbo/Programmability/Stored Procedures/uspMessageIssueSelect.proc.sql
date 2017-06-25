CREATE PROC [dbo].[uspMessageIssueSelect] 
    @MessageIssueID UNIQUEIDENTIFIER
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [MessageIssueID], [ConversationMessageID], [IssueID], [Score], [Value], [Deleted], [DeletedBy], [DeletedOn] 
	FROM   [dbo].[MessageIssue] 
	WHERE  ([MessageIssueID] = @MessageIssueID OR @MessageIssueID IS NULL) 

	COMMIT