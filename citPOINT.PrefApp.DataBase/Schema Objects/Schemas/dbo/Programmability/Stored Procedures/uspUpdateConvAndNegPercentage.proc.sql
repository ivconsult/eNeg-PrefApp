create PROCEDURE [dbo].[uspUpdateConvAndNegPercentage]
	@ConversationMessageID uniqueidentifier
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	Declare @PreferenceSetNegID uniqueidentifier
	BEGIN TRAN
	 
	/*-----------------------------------------------------------------------
	 ☼ ► Get Preference Set Neg For Current Message                        ☼ 
	-----------------------------------------------------------------------*/
	Set @PreferenceSetNegID=(SELECT Top 1 (PreferenceSetNeg.PreferenceSetNegID)
                             FROM   ConversationMessage INNER JOIN
                                    NegConversation ON ConversationMessage.NegConversationID = NegConversation.NegConversationID INNER JOIN
                                    PreferenceSetNeg ON NegConversation.PreferenceSetNegID = PreferenceSetNeg.PreferenceSetNegID
							 WHERE     (ConversationMessage.Deleted = 0) AND (ConversationMessage.ConversationMessageID = @ConversationMessageID))
	 
	/*-----------------------------------------------------------------------
	 ☼ ► Update Conversations by last message                              ☼ 
	-----------------------------------------------------------------------*/
	 
	 UPDATE [NegConversation]
	 SET 
	 [Percentage]=(ISNULL((SELECT TOP 1 Percentage 
						   FROM ConversationMessage 
						   WHERE Deleted=0 and 
								 RatedDate is not null  and 
								 ConversationMessage.NegConversationID=NegConversation.NegConversationID
						   ORDER BY  ConversationMessage.RatedDate DESC),0)) 
	WHERE Deleted=0 and PreferenceSetNegID=@PreferenceSetNegID;
	/*-----------------------------------------------------------------------
	 ☼ ► Update Preference Set Neg By Best message                         ☼ 
	-----------------------------------------------------------------------*/

	 UPDATE [PreferenceSetNeg] 
	 SET 
	 [Percentage]=(ISNULL((SELECT MAX(ConversationMessage.Percentage) 
						   FROM ConversationMessage INNER JOIN
								NegConversation ON ConversationMessage.NegConversationID = NegConversation.NegConversationID INNER JOIN
								PreferenceSetNeg T ON NegConversation.PreferenceSetNegID = T.PreferenceSetNegID
						   WHERE (ConversationMessage.Deleted = 0) AND (ConversationMessage.RatedDate IS NOT NULL)
						   AND [PreferenceSetNeg].PreferenceSetNegID=T.[PreferenceSetNegID]),0)) 
	WHERE Deleted=0 And PreferenceSetNegID= @PreferenceSetNegID;

    COMMIT 