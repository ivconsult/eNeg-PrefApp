CREATE PROC [dbo].[uspMessageOptionIssueUpdate] 
    @MessageOptionIssueID uniqueidentifier,
    @MessageIssueID uniqueidentifier,
    @OptionIssueID uniqueidentifier,
    @Deleted bit,
    @DeletedBy uniqueidentifier,
    @DeletedOn datetime
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[MessageOptionIssue]
	SET    [MessageOptionIssueID] = @MessageOptionIssueID, [MessageIssueID] = @MessageIssueID, [OptionIssueID] = @OptionIssueID, [Deleted] = @Deleted, [DeletedBy] = @DeletedBy, [DeletedOn] = @DeletedOn
	WHERE  [MessageOptionIssueID] = @MessageOptionIssueID
	
	-- Begin Return Select <- do not remove
	SELECT [MessageOptionIssueID], [MessageIssueID], [OptionIssueID], [Deleted], [DeletedBy], [DeletedOn]
	FROM   [dbo].[MessageOptionIssue]
	WHERE  [MessageOptionIssueID] = @MessageOptionIssueID	
	-- End Return Select <- do not remove

	COMMIT TRAN