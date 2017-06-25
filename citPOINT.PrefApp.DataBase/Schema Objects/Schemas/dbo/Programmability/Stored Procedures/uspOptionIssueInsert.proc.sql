CREATE PROC [dbo].[uspOptionIssueInsert] 
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
	
	INSERT INTO [dbo].[OptionIssue] ([OptionIssueID], [OptionIssueValue], [IssueID], [OptionIssueWeight], [Deleted], [DeletedBy], [DeletedOn])
	SELECT @OptionIssueID, @OptionIssueValue, @IssueID, @OptionIssueWeight, @Deleted, @DeletedBy, @DeletedOn
	
	-- Begin Return Select <- do not remove
	SELECT [OptionIssueID], [OptionIssueValue], [IssueID], [OptionIssueWeight], [Deleted], [DeletedBy], [DeletedOn]
	FROM   [dbo].[OptionIssue]
	WHERE  [OptionIssueID] = @OptionIssueID
	-- End Return Select <- do not remove
               
	COMMIT