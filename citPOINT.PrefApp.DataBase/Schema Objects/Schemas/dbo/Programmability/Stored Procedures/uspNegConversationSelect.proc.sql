CREATE PROCEDURE [dbo].[uspNegConversationSelect]
	 @NegConversationID UNIQUEIDENTIFIER
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [NegConversationID], [ConversationID], [Percentage], [PreferenceSetNegID] , [Deleted], [DeletedBy], [DeletedOn] 
	FROM   [dbo].[NegConversation] 
	WHERE  ([NegConversationID] = @NegConversationID OR @NegConversationID IS NULL) 

	COMMIT