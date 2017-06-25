CREATE TABLE [dbo].[MessageLaterRatedIssue]
(
	MessageLaterRatedIssueID uniqueidentifier primary key,
	MessageIssueID uniqueidentifier references [MessageIssue](MessageIssueID) not null,
	LaterRatedIssueID uniqueidentifier references [LaterRatedIssue](LaterRatedIssueID) not null,
	Deleted bit,
	DeletedBy uniqueidentifier,
	DeletedOn Datetime
)
