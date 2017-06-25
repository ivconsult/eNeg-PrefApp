CREATE PROCEDURE [dbo].[uspLaterRatedIssueInsert]
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
	
	INSERT INTO [dbo].[LaterRatedIssue] 
		  ([LaterRatedIssueID], [LaterRatedIssueValue], [IssueID], [LaterRatedIssueWeight], [Deleted], [DeletedBy], [DeletedOn])
	SELECT @LaterRatedIssueID, @LaterRatedIssueValue, @IssueID, @LaterRatedIssueWeight, @Deleted, @DeletedBy, @DeletedOn
	
	-- Begin Return Select <- do not remove
	SELECT [LaterRatedIssueID], [LaterRatedIssueValue], [IssueID], [LaterRatedIssueWeight], [Deleted], [DeletedBy], [DeletedOn]
	FROM   [dbo].[LaterRatedIssue]
	WHERE  [LaterRatedIssueID] = @LaterRatedIssueID
	-- End Return Select <- do not remove
               
	COMMIT