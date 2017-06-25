CREATE PROCEDURE [dbo].[uspNegConversationUpdate]
	@NegConversationID uniqueidentifier,
	@ConversationID uniqueidentifier,
    @Percentage  decimal(18, 2) ,
    @PreferenceSetNegID uniqueidentifier,
	@Deleted bit,
    @DeletedBy uniqueidentifier,
    @DeletedOn datetime
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[NegConversation]
	SET    [NegConversationID] = @NegConversationID, [ConversationID] = @ConversationID, [Percentage] = @Percentage, [PreferenceSetNegID] = @PreferenceSetNegID, [Deleted] = @Deleted, [DeletedBy] = @DeletedBy, [DeletedOn] = @DeletedOn
	WHERE  [NegConversationID] = @NegConversationID
	
	-- Begin Return Select <- do not remove
	SELECT [NegConversationID], [ConversationID], [Percentage], [PreferenceSetNegID], [Deleted], [DeletedBy], [DeletedOn] 
	FROM   [dbo].[NegConversation]
	WHERE  [NegConversationID] = @NegConversationID
	-- End Return Select <- do not remove

	COMMIT TRAN
