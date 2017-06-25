CREATE PROC [dbo].[uspConversationMessageSelect] 
    @ConversationMessageID UNIQUEIDENTIFIER
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [ConversationMessageID], [MessageID], [Percentage], [NegConversationID], [IsSent], [IsExceedVariation], [Deleted], [DeletedBy], [DeletedOn], [RatedDate] 
	FROM   [dbo].[ConversationMessage] 
	WHERE  ([ConversationMessageID] = @ConversationMessageID OR @ConversationMessageID IS NULL) 

	COMMIT