
Create PROC [dbo].[uspConversationMessageSelectLast2OffersIDs] 
    @NegotiationID  UNIQUEIDENTIFIER,
    @ConversationID UNIQUEIDENTIFIER,
    @OfferType      TINYINT
AS 

/*----------------------------------------------------------
>Some details about SP.
------------------------------------------------------------
Getting the last 2 offers Rate depend on
 
 >>Offer Type 
	
	My offers Only          @OfferType=[1 Own]
	CounterPart offers Only @OfferType=[2 CounterPart]
	Mix offers Both         @OfferType=[3 Mix]

 >>For Certain Convcersation or for whole Negotiation

*/

  SET nocount ON 
  SET xact_abort ON 

--=============================================================
--  Calculating the types of messages depend on Offer Type
--=============================================================
  declare @OwnTop int=0;
  declare @CounterPartTop int=0
    
  IF( @OfferType = 1 ) --Own offers ony (Sent only) 
	SET @OwnTop=1; 
  ELSE 
	  IF( @OfferType = 2 ) --Counterpart Offers ony Received
		SET @CounterPartTop=1; 
	  ELSE 
		BEGIN --Mixed
			SET @OwnTop=1; 
			SET @CounterPartTop=1; 
		END 
--=============================================================

Select  ConversationMessageID From
(
Select  ConversationMessageID,RatedDate From (
       --Sent Messages [My Messages Own]
	  SELECT top(@OwnTop) ConversationMessage.ConversationMessageID,
			 ConversationMessage.RatedDate 
	  FROM   ConversationMessage 
			 INNER JOIN NegConversation 
			   ON ConversationMessage.NegConversationID = 
				  NegConversation.NegConversationID 
			 INNER JOIN PreferenceSetNeg 
			   ON NegConversation.PreferenceSetNegID = 
				  PreferenceSetNeg.PreferenceSetNegID 
	  WHERE  ( ConversationMessage.Deleted = 0 ) 
			 AND ( NegConversation.Deleted = 0 ) 
			 AND ( PreferenceSetNeg.Deleted = 0 ) 
			 AND ( ConversationMessage.RatedDate IS NOT NULL ) 
			 AND ( ConversationMessage.IsSent =1) 
			 AND (@NegotiationID is null or   PreferenceSetNeg.NegID = @NegotiationID ) 
			 AND (@ConversationID is null or NegConversation.ConversationID = @ConversationID ) 
	  order by ConversationMessage.RatedDate Desc) T
	  
	  union All
	  --Received Messages [Counterpart Messages]
	  Select  ConversationMessageID,RatedDate From (
	  SELECT top (@CounterPartTop) ConversationMessage.ConversationMessageID,
								   ConversationMessage.RatedDate 
	  FROM   ConversationMessage 
			 INNER JOIN NegConversation 
			   ON ConversationMessage.NegConversationID = 
				  NegConversation.NegConversationID 
			 INNER JOIN PreferenceSetNeg 
			   ON NegConversation.PreferenceSetNegID = 
				  PreferenceSetNeg.PreferenceSetNegID 
	  WHERE  ( ConversationMessage.Deleted = 0 ) 
			 AND ( NegConversation.Deleted = 0 ) 
			 AND ( PreferenceSetNeg.Deleted = 0 ) 
			 AND ( ConversationMessage.RatedDate IS NOT NULL ) 
			 AND ( ConversationMessage.IsSent =0) 
			 AND (@NegotiationID  is null or   PreferenceSetNeg.NegID = @NegotiationID ) 
			 AND (@ConversationID is null or NegConversation.ConversationID = @ConversationID ) 
	  order by ConversationMessage.RatedDate  Desc) T
) Y
order by Y.RatedDate Desc   
