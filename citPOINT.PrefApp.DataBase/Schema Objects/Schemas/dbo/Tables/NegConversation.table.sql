CREATE TABLE [dbo].[NegConversation]
(
	NegConversationID uniqueidentifier primary key,
	ConversationID uniqueidentifier,
	Percentage   decimal(18, 2) not null,
	PreferenceSetNegID uniqueidentifier references PreferenceSetNeg(PreferenceSetNegID) not null,
	Deleted bit,
	DeletedBy uniqueidentifier,
	DeletedOn Datetime
)

