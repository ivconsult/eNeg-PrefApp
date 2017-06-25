CREATE TABLE [dbo].[NumericIssue]
(
	NumericIssueID uniqueidentifier primary key,
	IssueID uniqueidentifier references [Issue](IssueID) not null,
	MinimumValue   decimal(18, 2) not null,
	MaximumValue   decimal(18, 2) not null,
	OptimumValueStart   decimal(18, 2) not null,
	OptimumValueEnd   decimal(18, 2) not null,
	MinimumOperator tinyint not null,
	MaximumOperator tinyint not null,
	Unit nvarchar(100) not null,
	Deleted bit,
	DeletedBy uniqueidentifier,
	DeletedOn Datetime
)
