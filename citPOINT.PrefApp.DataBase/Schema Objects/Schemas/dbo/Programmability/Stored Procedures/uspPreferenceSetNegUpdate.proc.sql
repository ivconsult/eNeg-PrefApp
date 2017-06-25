CREATE PROC [dbo].[uspPreferenceSetNegUpdate] 
	@PreferenceSetNegID uniqueidentifier,
    @NegID uniqueidentifier,
    @Percentage  decimal(18, 2) ,
    @PreferenceSetID uniqueidentifier,
	@NegotiationName nvarchar(150),
	@StatusID uniqueidentifier,
    @Deleted bit,
    @DeletedBy uniqueidentifier,
    @DeletedOn datetime
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[PreferenceSetNeg]
	SET    [PreferenceSetNegID] = @PreferenceSetNegID, 
	       [NegID]              = @NegID, 
		   [Percentage]         = @Percentage, 
		   [PreferenceSetID]    = @PreferenceSetID, 
		   [NegotiationName]    = @NegotiationName,
		   [StatusID]           = @StatusID,
		   [Deleted]            = @Deleted, 
		   [DeletedBy]          = @DeletedBy, 
		   [DeletedOn]          = @DeletedOn
	WHERE  [PreferenceSetNegID] = @PreferenceSetNegID
	
	
	-- Begin Return Select <- do not remove
	SELECT [PreferenceSetNegID], [NegID], [Percentage], [PreferenceSetID],[NegotiationName],[StatusID],[Deleted], [DeletedBy], [DeletedOn]
	FROM   [dbo].[PreferenceSetNeg]
	WHERE  [PreferenceSetNegID] = @PreferenceSetNegID	
	-- End Return Select <- do not remove

	COMMIT TRAN