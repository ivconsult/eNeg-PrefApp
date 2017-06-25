CREATE PROCEDURE [dbo].[uspNegConversationInsert]
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
	
	INSERT INTO [dbo].[NegConversation] ([NegConversationID], [ConversationID], [Percentage], [PreferenceSetNegID], [Deleted], [DeletedBy], [DeletedOn])
	SELECT @NegConversationID, @ConversationID, @Percentage, @PreferenceSetNegID, @Deleted, @DeletedBy, @DeletedOn
	
	-- Begin Return Select <- do not remove
	SELECT [NegConversationID], [ConversationID], [Percentage], [PreferenceSetNegID], [Deleted], [DeletedBy], [DeletedOn] 
	FROM   [dbo].[NegConversation]
	WHERE  [NegConversationID] = @NegConversationID
	-- End Return Select <- do not remove
               
	COMMIT