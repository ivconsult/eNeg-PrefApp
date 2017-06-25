CREATE PROC [dbo].[uspOptionIssueUpdate] 
    @OptionIssueID uniqueidentifier,
    @OptionIssueValue nvarchar(100),
    @IssueID uniqueidentifier,
    @OptionIssueWeight  decimal(18, 2) ,
    @Deleted bit,
    @DeletedBy uniqueidentifier,
    @DeletedOn datetime
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[OptionIssue]
	SET    [OptionIssueID] = @OptionIssueID, [OptionIssueValue] = @OptionIssueValue, [IssueID] = @IssueID, [OptionIssueWeight] = @OptionIssueWeight, [Deleted] = @Deleted, [DeletedBy] = @DeletedBy, [DeletedOn] = @DeletedOn
	WHERE  [OptionIssueID] = @OptionIssueID
	
	-- Begin Return Select <- do not remove
	SELECT [OptionIssueID], [OptionIssueValue], [IssueID], [OptionIssueWeight], [Deleted], [DeletedBy], [DeletedOn]
	FROM   [dbo].[OptionIssue]
	WHERE  [OptionIssueID] = @OptionIssueID	
	-- End Return Select <- do not remove

	COMMIT TRAN