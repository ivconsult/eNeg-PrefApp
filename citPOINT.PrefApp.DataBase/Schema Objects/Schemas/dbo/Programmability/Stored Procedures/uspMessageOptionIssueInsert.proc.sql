CREATE PROC [dbo].[uspMessageOptionIssueInsert] 
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
	
	INSERT INTO [dbo].[MessageOptionIssue] ([MessageOptionIssueID], [MessageIssueID], [OptionIssueID], [Deleted], [DeletedBy], [DeletedOn])
	SELECT @MessageOptionIssueID, @MessageIssueID, @OptionIssueID, @Deleted, @DeletedBy, @DeletedOn
	
	-- Begin Return Select <- do not remove
	SELECT [MessageOptionIssueID], [MessageIssueID], [OptionIssueID], [Deleted], [DeletedBy], [DeletedOn]
	FROM   [dbo].[MessageOptionIssue]
	WHERE  [MessageOptionIssueID] = @MessageOptionIssueID
	-- End Return Select <- do not remove
               
	COMMIT