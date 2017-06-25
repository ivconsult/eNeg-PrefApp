CREATE TABLE [dbo].[MessageIssue]
(
	MessageIssueID uniqueidentifier primary key,
	ConversationMessageID uniqueidentifier references [ConversationMessage](ConversationMessageID) not null,
	IssueID uniqueidentifier references [Issue](IssueID) not null,
	Score decimal(18,2),
	Value nvarchar(300),
	Deleted bit,
	DeletedBy uniqueidentifier,
	DeletedOn Datetime
)
