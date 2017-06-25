CREATE TABLE [dbo].[MessageOptionIssue]
(
	MessageOptionIssueID uniqueidentifier primary key,
	MessageIssueID uniqueidentifier references [MessageIssue](MessageIssueID) not null,
	OptionIssueID uniqueidentifier references [OptionIssue](OptionIssueID) not null,
	Deleted bit,
	DeletedBy uniqueidentifier,
	DeletedOn Datetime
)
