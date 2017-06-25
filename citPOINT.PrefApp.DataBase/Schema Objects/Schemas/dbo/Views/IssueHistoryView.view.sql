CREATE VIEW [dbo].[IssueHistoryView]
	AS 
		SELECT Issue.IssueName, 
       PreferenceSet.PreferenceSetName, 
       PreferenceSetNeg.NegotiationName, 
       Issue.IssueWeight,
       PreferenceSetNeg.NegID --Specially for Exclude current Negotiation 
FROM   Issue 
       INNER JOIN PreferenceSet 
               ON Issue.PreferenceSetID = PreferenceSet.PreferenceSetID 
       INNER JOIN PreferenceSetNeg 
               ON PreferenceSet.PreferenceSetID = 
                  PreferenceSetNeg.PreferenceSetID 
WHERE  ( PreferenceSetNeg.Deleted = 0 ) 
       AND ( PreferenceSet.Deleted = 0 ) 
       AND ( Issue.Deleted = 0 )  