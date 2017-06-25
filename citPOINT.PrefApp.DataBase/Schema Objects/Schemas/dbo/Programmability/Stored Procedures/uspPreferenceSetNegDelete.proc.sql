CREATE PROCEDURE [dbo].[uspPreferenceSetNegDelete]
	@PreferenceSetNegID uniqueidentifier
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[PreferenceSetNeg]
	SET 
		Deleted=1,
		DeletedOn=GETDATE()
	WHERE  [PreferenceSetNegID] = @PreferenceSetNegID

	COMMIT