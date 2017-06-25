CREATE PROC [dbo].[uspMessageLaterRatedIssueUpdate] 
    @MessageLaterRatedIssueID uniqueidentifier,
    @MessageIssueID uniqueidentifier,
    @LaterRatedIssueID uniqueidentifier,
    @Deleted bit,
    @DeletedBy uniqueidentifier,
    @DeletedOn datetime
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[MessageLaterRatedIssue]
	SET    [MessageLaterRatedIssueID] = @MessageLaterRatedIssueID, [MessageIssueID] = @MessageIssueID, [LaterRatedIssueID] = @LaterRatedIssueID, [Deleted] = @Deleted, [DeletedBy] = @DeletedBy, [DeletedOn] = @DeletedOn
	WHERE  [MessageLaterRatedIssueID] = @MessageLaterRatedIssueID
	
	-- Begin Return Select <- do not remove
	SELECT [MessageLaterRatedIssueID], [MessageIssueID], [LaterRatedIssueID], [Deleted], [DeletedBy], [DeletedOn]
	FROM   [dbo].[MessageLaterRatedIssue]
	WHERE  [MessageLaterRatedIssueID] = @MessageLaterRatedIssueID	
	-- End Return Select <- do not remove

	COMMIT TRAN