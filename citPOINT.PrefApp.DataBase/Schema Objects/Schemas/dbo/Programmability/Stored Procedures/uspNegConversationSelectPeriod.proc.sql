CREATE PROCEDURE [dbo].[uspNegConversationSelectPeriod]
	 @ConversationID  UNIQUEIDENTIFIER 
AS 
  SET nocount ON 
  SET xact_abort ON 

  SELECT MIN(conversationmessage.deletedon) AS MinConversationDate, 
         MAX(conversationmessage.deletedon) AS MaxConversationDate 
  FROM   negconversation 
         INNER JOIN conversationmessage 
           ON negconversation.negconversationid = 
              conversationmessage.negconversationid 
  WHERE  ( negconversation.conversationid = @ConversationID ) 
         AND ( negconversation.deleted = 0 ) 
         AND conversationmessage.deleted = 0
         
         