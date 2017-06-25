CREATE PROCEDURE [dbo].[uspConversationMessageSelectOfferDetails] 
    @ConversationMessageID  UNIQUEIDENTIFIER
AS 

/*----------------------------------------------------------
>Some details about SP.
------------------------------------------------------------

*/

  SET nocount ON 
  SET xact_abort ON 

	SELECT TOP (100) PERCENT ROW_NUMBER() OVER(ORDER BY ConversationMessage.MessageID,  Issue.DeletedOn, Issue.IssueID) AS Rank,
	                         MessageIssue.IssueID, 
	                         Issue.IssueTypeID,
							 ( CASE 
								 WHEN ( OptionIssue.OptionIssueValue IS NOT NULL ) 
								   THEN OptionIssue.OptionIssueValue 
								 WHEN ( LaterRatedIssue.LaterRatedIssueValue IS NOT NULL ) 
								   THEN LaterRatedIssue.LaterRatedIssueValue 
								 ELSE MessageIssue.VALUE 
							   END ) Value, 
	                         
							 MessageIssue.Score, 
	                         
							 ( CASE 
								 WHEN ( OptionIssue.OptionIssueWeight IS NOT NULL ) 
								   THEN OptionIssue.OptionIssueWeight 
								 ELSE LaterRatedIssue.LaterRatedIssueWeight 
							   END ) OptionRate, 
	                         
							 ( CASE 
								 WHEN ( OptionIssue.OptionIssueID IS NOT NULL ) 
								   THEN OptionIssue.OptionIssueID 
								 ELSE LaterRatedIssue.LaterRatedIssueID 
							   END ) OptionID 
	FROM   LaterRatedIssue 
		   LEFT OUTER JOIN MessageLaterRatedIssue 
			 ON LaterRatedIssue.LaterRatedIssueID = 
				MessageLaterRatedIssue.LaterRatedIssueID 
		   RIGHT OUTER JOIN ConversationMessage 
							INNER JOIN MessageIssue 
							  ON ConversationMessage.ConversationMessageID = 
								 MessageIssue.ConversationMessageID 
							INNER JOIN Issue 
							  ON MessageIssue.IssueID = Issue.IssueID 
			 ON MessageLaterRatedIssue.MessageIssueID = MessageIssue.MessageIssueID 
		   LEFT OUTER JOIN MessageOptionIssue 
						   RIGHT OUTER JOIN OptionIssue 
							 ON MessageOptionIssue.OptionIssueID = 
								OptionIssue.OptionIssueID 
			 ON MessageIssue.MessageIssueID = MessageOptionIssue.MessageIssueID 
	WHERE  ( ConversationMessage.ConversationMessageID = @ConversationMessageID ) AND 
	       ( ConversationMessage.Deleted = 0) AND
	       ( MessageIssue.Deleted = 0) and
	       (MessageLaterRatedIssue.MessageIssueID IS NULL OR ( MessageLaterRatedIssue.MessageIssueID IS NOT NULL AND MessageLaterRatedIssue.Deleted=0))
	       and
	       (MessageOptionIssue.MessageIssueID IS NULL OR ( MessageOptionIssue.MessageIssueID IS NOT NULL AND MessageOptionIssue.Deleted=0))

	ORDER BY  ConversationMessage.MessageID,  Issue.DeletedOn, Issue.IssueID