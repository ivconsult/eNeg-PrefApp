CREATE PROC [dbo].[uspConversationMessageInsert] 
    @ConversationMessageID uniqueidentifier,
    @MessageID uniqueidentifier,
    @Percentage decimal(18, 2),
    @NegConversationID uniqueidentifier,
    @IsSent bit,
    @IsExceedVariation bit,
    @Deleted bit,
    @DeletedBy uniqueidentifier,
    @DeletedOn datetime,
    @RatedDate datetime
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[ConversationMessage] ([ConversationMessageID], [MessageID], [Percentage], [NegConversationID], [IsSent], [IsExceedVariation], [Deleted], [DeletedBy], [DeletedOn], [RatedDate])
	SELECT @ConversationMessageID, @MessageID, @Percentage, @NegConversationID, @IsSent, @IsExceedVariation, @Deleted, @DeletedBy, @DeletedOn, @RatedDate
	
	-- Begin Return Select <- do not remove
	SELECT [ConversationMessageID], [MessageID], [Percentage], [NegConversationID], [IsSent], [IsExceedVariation], [Deleted], [DeletedBy], [DeletedOn], [RatedDate]
	FROM   [dbo].[ConversationMessage]
	WHERE  [ConversationMessageID] = @ConversationMessageID
	-- End Return Select <- do not remove
               
	COMMIT