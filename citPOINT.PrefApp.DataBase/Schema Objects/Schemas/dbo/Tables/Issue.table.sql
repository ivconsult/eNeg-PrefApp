CREATE TABLE [dbo].[Issue]
(
	IssueID uniqueidentifier primary key,
	IssueName nvarchar(300) not null,
	PreferenceSetID uniqueidentifier references PreferenceSet(PreferenceSetID) not null,
	IssueTypeID uniqueidentifier references [IssueType](IssueTypeID) not null,
	IssueWeight  decimal(18, 2)  not null,
	Deleted bit,
	DeletedBy uniqueidentifier,
	DeletedOn Datetime
)
