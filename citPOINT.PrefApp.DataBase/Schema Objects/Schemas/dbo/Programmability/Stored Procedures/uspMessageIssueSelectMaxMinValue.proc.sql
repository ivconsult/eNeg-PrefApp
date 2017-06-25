Create PROC [dbo].[uspMessageIssueSelectMaxMinValue] 
    @preferenceSetNegID UNIQUEIDENTIFIER,
    @IssueTypeID        UNIQUEIDENTIFIER,
    @IssueID            UNIQUEIDENTIFIER,
    @IsMaxValue         bit
    
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
Declare @Max_Value as decimal;
Declare @Min_Value as decimal;


	BEGIN TRAN


SELECT   @Max_Value=MAX(CAST(MessageIssue.Value AS decimal)),@Min_Value=Min(CAST(MessageIssue.Value AS decimal))
FROM         MessageIssue INNER JOIN
                      ConversationMessage ON MessageIssue.ConversationMessageID = ConversationMessage.ConversationMessageID INNER JOIN
                      NegConversation ON ConversationMessage.NegConversationID = NegConversation.NegConversationID INNER JOIN
                      PreferenceSetNeg ON NegConversation.PreferenceSetNegID = PreferenceSetNeg.PreferenceSetNegID INNER JOIN
                      Issue ON MessageIssue.IssueID = Issue.IssueID
where Issue.IssueTypeID=@IssueTypeID AND
      [PreferenceSetNeg].[PreferenceSetNegID]= @preferenceSetNegID    AND
      [Issue].[IssueID]= @IssueID  AND
      MessageIssue.Deleted=0;
      

if(@IsMaxValue=1)
begin
Select  @Max_Value [Value]
End


if(@IsMaxValue=0)
begin
Select  @Min_Value [Value]
End

COMMIT