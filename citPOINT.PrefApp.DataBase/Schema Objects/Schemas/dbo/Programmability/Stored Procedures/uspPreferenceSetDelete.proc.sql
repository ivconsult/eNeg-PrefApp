
CREATE PROC [dbo].[uspPreferenceSetDelete] 
    @PreferenceSetID uniqueidentifier
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	UPDATE [dbo].[PreferenceSet] 
	SET 
		Deleted=1,
		DeletedOn=GETDATE()
	WHERE  [PreferenceSetID] = @PreferenceSetID;

	/*Updating Pref-Organization As Deleted Also*/
	UPDATE  [dbo].[PreferenceSetOrganization]
	SET 
			Deleted=1,
			DeletedOn=GETDATE()
	WHERE  [PreferenceSetID] = @PreferenceSetID;

	/*Updating Issues As Deleted Also*/
	UPDATE [dbo].[Issue]
	SET 
	Deleted=1,
	DeletedOn=GETDATE()
	WHERE  [PreferenceSetID] = @PreferenceSetID;
	
	/*Updating PreferenceSetNeg As Deleted Also*/
	UPDATE [dbo].[PreferenceSetNeg] 
	SET 
	Deleted=1,
	DeletedOn=GETDATE()
	WHERE  [PreferenceSetID] = @PreferenceSetID;
	COMMIT