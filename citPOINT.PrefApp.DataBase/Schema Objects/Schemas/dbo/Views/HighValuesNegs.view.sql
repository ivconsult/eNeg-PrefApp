CREATE VIEW [dbo].[HighValuesNegs]
	AS 
  SELECT PreferenceSetNeg.NegotiationName, 
         PreferenceSet.PreferenceSetName, 
         COUNT(NegConversation.ConversationID) AS CounterPartsCount ,
         PreferenceSetNeg.NegID
  FROM   PreferenceSetNeg 
         INNER JOIN NegConversation 
                 ON PreferenceSetNeg.PreferenceSetNegID = 
                    NegConversation.PreferenceSetNegID 
         INNER JOIN PreferenceSet 
                 ON PreferenceSetNeg.PreferenceSetID = 
                    PreferenceSet.PreferenceSetID 
  WHERE  ( PreferenceSet.Deleted = 0 ) 
         AND ( NegConversation.Deleted = 0 ) 
         AND ( PreferenceSetNeg.Deleted = 0 ) 
         AND ( PreferenceSetNeg.StatusID = 
               'dfcea50d-18a2-4e41-9be8-1673e88101c4' 
             ) /* Find Closed Negotiations Only */ 
         AND ( PreferenceSetNeg.Percentage > 70 
             /* Negotiations that go moore 70% as percentage */ ) 
  GROUP  BY PreferenceSetNeg.NegotiationName, 
            PreferenceSet.PreferenceSetName ,
            PreferenceSetNeg.NegID 