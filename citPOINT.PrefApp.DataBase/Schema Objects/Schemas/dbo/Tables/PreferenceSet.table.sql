CREATE TABLE [dbo].[PreferenceSet]
(
	PreferenceSetID uniqueidentifier primary key,
	PreferenceSetName nvarchar(300) not null,
	Checkvariation	bit	not null,
	VariationValue	decimal(18, 2) not null default 0,
	MaxPercentage	decimal(18, 2) not null default 100,
	UserID uniqueidentifier not null,
	MainPreferenceSetID uniqueidentifier references MainPreferenceSet(MainPreferenceSetID) not null,
	Deleted bit,
	DeletedBy uniqueidentifier,
	DeletedOn Datetime
)
