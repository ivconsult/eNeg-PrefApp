CREATE PROCEDURE [dbo].[uspPreferenceSetNegSelectPeriod]
	 @NegotiationID  UNIQUEIDENTIFIER 
AS 
  SET nocount ON 
  SET xact_abort ON 

SELECT MIN(ConversationMessage.DeletedOn) AS MinConversationDate,
       MAX(ConversationMessage.DeletedOn) AS MaxConversationDate
FROM   NegConversation
       INNER JOIN ConversationMessage
       ON     NegConversation.NegConversationID = ConversationMessage.NegConversationID
       INNER JOIN PreferenceSetNeg
       ON     NegConversation.PreferenceSetNegID = PreferenceSetNeg.PreferenceSetNegID

WHERE  (NegConversation.Deleted     = 0      ) AND
       (ConversationMessage.Deleted = 0      ) AND
       (PreferenceSetNeg.Deleted    = 0      ) AND
       (PreferenceSetNeg.NegID=@NegotiationID)