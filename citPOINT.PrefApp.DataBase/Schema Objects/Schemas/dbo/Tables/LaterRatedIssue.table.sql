CREATE TABLE [dbo].[LaterRatedIssue]
(
	LaterRatedIssueID uniqueidentifier primary key,
	LaterRatedIssueValue nvarchar(100) not null,
	IssueID uniqueidentifier references [Issue](IssueID) not null,
	LaterRatedIssueWeight  decimal(18, 2)  not null,
	Deleted bit,
	DeletedBy uniqueidentifier,
	DeletedOn Datetime
)
