CREATE PROC [dbo].[uspPreferenceSetOrganizationDelete] 
    @PreferenceSetOrganizationID uniqueidentifier
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE  [dbo].[PreferenceSetOrganization]
	SET 
			Deleted=1,
			DeletedOn=GETDATE()
	WHERE  [PreferenceSetOrganizationID] = @PreferenceSetOrganizationID

	COMMIT
GO