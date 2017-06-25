CREATE TABLE [dbo].[OptionIssue]
(
	OptionIssueID uniqueidentifier primary key,
	OptionIssueValue nvarchar(100) not null,
	IssueID uniqueidentifier references [Issue](IssueID) not null,
	OptionIssueWeight   decimal(18, 2) not null,
	Deleted bit,
	DeletedBy uniqueidentifier,
	DeletedOn Datetime
)
