CREATE TABLE [dbo].[ConversationMessage]
(
	ConversationMessageID uniqueidentifier primary key,
	MessageID uniqueidentifier,
	Percentage   decimal(18, 2) null,
	NegConversationID uniqueidentifier references NegConversation(NegConversationID) not null,
	IsSent bit,
	IsExceedVariation bit not null default 0,
	Deleted bit,
	DeletedBy uniqueidentifier,
	DeletedOn Datetime,
	RatedDate Datetime
)
