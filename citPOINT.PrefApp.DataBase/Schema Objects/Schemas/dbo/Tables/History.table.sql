CREATE TABLE [dbo].[History]
(
	SN uniqueidentifier primary key,
	TableName nvarchar(50) not null,
	ActionTypeID uniqueidentifier references ActionType(ActionTypeID) not null,
	OldValue xml not null,
	NewValue xml not null,
	DoneBy uniqueidentifier not null,
	ActionDate datetime not null
)

