CREATE PROCEDURE [dbo].[uspLaterRatedIssueUpdate]
	@LaterRatedIssueID uniqueidentifier,
    @LaterRatedIssueValue nvarchar(100),
    @IssueID uniqueidentifier,
    @LaterRatedIssueWeight  decimal(18, 2) ,
    @Deleted bit,
    @DeletedBy uniqueidentifier,
    @DeletedOn datetime

AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[LaterRatedIssue]
	SET    [LaterRatedIssueID] = @LaterRatedIssueID, [LaterRatedIssueValue] = @LaterRatedIssueValue, [IssueID] = @IssueID, 
	       [LaterRatedIssueWeight] = @LaterRatedIssueWeight, [Deleted] = @Deleted, [DeletedBy]  = @DeletedBy , [DeletedOn] = @DeletedOn
	WHERE  [LaterRatedIssueID] = @LaterRatedIssueID
	
	-- Begin Return Select <- do not remove
	SELECT [LaterRatedIssueID], [LaterRatedIssueValue], [IssueID], [LaterRatedIssueWeight], [Deleted], [DeletedBy], [DeletedOn]
	FROM   [dbo].[LaterRatedIssue]
	WHERE  [LaterRatedIssueID] = @LaterRatedIssueID	
	-- End Return Select <- do not remove

	COMMIT TRAN