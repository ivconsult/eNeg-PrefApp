CREATE PROCEDURE [dbo].[uspConversationMessageDelete]
	 @ConversationMessageID uniqueidentifier
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

		UPDATE [dbo].[ConversationMessage] 
		SET 
			Deleted=1,
			DeletedOn=GETDATE()
		WHERE  [ConversationMessageID] = @ConversationMessageID

	COMMIT