CREATE PROC [dbo].[uspMessageIssueInsert] 
    @MessageIssueID uniqueidentifier,
    @ConversationMessageID uniqueidentifier,
    @IssueID uniqueidentifier,
    @Score  decimal(18, 2) ,
    @Value nvarchar(300),
    @Deleted bit,
    @DeletedBy uniqueidentifier,
    @DeletedOn datetime
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[MessageIssue] ([MessageIssueID], [ConversationMessageID], [IssueID], [Score], [Value], [Deleted], [DeletedBy], [DeletedOn])
	SELECT @MessageIssueID, @ConversationMessageID, @IssueID, @Score, @Value, @Deleted, @DeletedBy, @DeletedOn
	
	-- Begin Return Select <- do not remove
	SELECT [MessageIssueID], [ConversationMessageID], [IssueID], [Score], [Value], [Deleted], [DeletedBy], [DeletedOn]
	FROM   [dbo].[MessageIssue]
	WHERE  [MessageIssueID] = @MessageIssueID
	-- End Return Select <- do not remove
               
	COMMIT