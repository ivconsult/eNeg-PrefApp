CREATE PROC [dbo].[uspPreferenceSetOrganizationInsert] 
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
	
	INSERT INTO [dbo].[PreferenceSetOrganization] ([PreferenceSetOrganizationID], [PreferenceSetID], [OrganizationID], [Deleted], [DeletedBy], [DeletedOn])
	SELECT @PreferenceSetOrganizationID, @PreferenceSetID, @OrganizationID, @Deleted, @DeletedBy, @DeletedOn
	
	-- Begin Return Select <- do not remove
	SELECT [PreferenceSetOrganizationID], [PreferenceSetID], [OrganizationID], [Deleted], [DeletedBy], [DeletedOn]
	FROM   [dbo].[PreferenceSetOrganization]
	WHERE  [PreferenceSetOrganizationID] = @PreferenceSetOrganizationID
	-- End Return Select <- do not remove
               
	COMMIT
GO