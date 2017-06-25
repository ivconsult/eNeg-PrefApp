CREATE PROC [dbo].[uspPreferenceSetOrganizationUpdate] 
    @PreferenceSetOrganizationID uniqueidentifier,
    @PreferenceSetID uniqueidentifier,
    @OrganizationID uniqueidentifier,
    @Deleted bit,
    @DeletedBy uniqueidentifier,
    @DeletedOn datetime
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[PreferenceSetOrganization]
	SET    [PreferenceSetOrganizationID] = @PreferenceSetOrganizationID, [PreferenceSetID] = @PreferenceSetID, [OrganizationID] = @OrganizationID, [Deleted] = @Deleted, [DeletedBy] = @DeletedBy, [DeletedOn] = @DeletedOn
	WHERE  [PreferenceSetOrganizationID] = @PreferenceSetOrganizationID
	
	-- Begin Return Select <- do not remove
	SELECT [PreferenceSetOrganizationID], [PreferenceSetID], [OrganizationID], [Deleted], [DeletedBy], [DeletedOn]
	FROM   [dbo].[PreferenceSetOrganization]
	WHERE  [PreferenceSetOrganizationID] = @PreferenceSetOrganizationID	
	-- End Return Select <- do not remove

	COMMIT TRAN
GO