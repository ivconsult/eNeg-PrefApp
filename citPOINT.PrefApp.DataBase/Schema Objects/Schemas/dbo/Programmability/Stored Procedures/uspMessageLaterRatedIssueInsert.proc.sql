CREATE PROC [dbo].[uspMessageLaterRatedIssueInsert] 
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
	
	INSERT INTO [dbo].[MessageLaterRatedIssue] ([MessageLaterRatedIssueID], [MessageIssueID], [LaterRatedIssueID], [Deleted], [DeletedBy], [DeletedOn])
	SELECT @MessageLaterRatedIssueID, @MessageIssueID, @LaterRatedIssueID, @Deleted, @DeletedBy, @DeletedOn
	
	-- Begin Return Select <- do not remove
	SELECT [MessageLaterRatedIssueID], [MessageIssueID], [LaterRatedIssueID], [Deleted], [DeletedBy], [DeletedOn]
	FROM   [dbo].[MessageLaterRatedIssue]
	WHERE  [MessageLaterRatedIssueID] = @MessageLaterRatedIssueID
	-- End Return Select <- do not remove
               
	COMMIT