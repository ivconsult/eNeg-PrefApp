CREATE PROCEDURE [dbo].[uspNegConversationDelete]
	 @NegConversationID uniqueidentifier
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

		UPDATE [dbo].[NegConversation]
		SET 
			Deleted=1,
			DeletedOn=GETDATE()
		WHERE  [NegConversationID] = @NegConversationID
	COMMIT
