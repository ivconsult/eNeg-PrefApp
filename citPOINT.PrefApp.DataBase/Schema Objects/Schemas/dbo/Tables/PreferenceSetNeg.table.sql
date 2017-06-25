CREATE TABLE [dbo].[PreferenceSetNeg]
(
	PreferenceSetNegID  uniqueidentifier primary key,
	NegID  uniqueidentifier not null,
	Percentage   decimal(18, 2) not null,
	PreferenceSetID uniqueidentifier references PreferenceSet(PreferenceSetID) not null,
	NegotiationName nvarchar(150) not null,
	StatusID uniqueidentifier,
	Deleted bit,
	DeletedBy uniqueidentifier,
	DeletedOn Datetime
)
