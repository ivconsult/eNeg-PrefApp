CREATE TABLE [dbo].[PreferenceSetOrganization](
	[PreferenceSetOrganizationID] [uniqueidentifier] NOT NULL,
	[PreferenceSetID] [uniqueidentifier] NOT NULL,
	[OrganizationID] [uniqueidentifier] NOT NULL,
	[Deleted] [bit] NOT NULL,
	[DeletedBy] [uniqueidentifier] NULL,
	[DeletedOn] [datetime] NULL,
 CONSTRAINT [PK_PreferenceSetOrganization] PRIMARY KEY CLUSTERED 
(
	[PreferenceSetOrganizationID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[PreferenceSetOrganization]  WITH CHECK ADD  CONSTRAINT [FK_PreferenceSetOrganization_PreferenceSet] FOREIGN KEY([PreferenceSetID])
REFERENCES [dbo].[PreferenceSet] ([PreferenceSetID])
GO

ALTER TABLE [dbo].[PreferenceSetOrganization] CHECK CONSTRAINT [FK_PreferenceSetOrganization_PreferenceSet]
GO



