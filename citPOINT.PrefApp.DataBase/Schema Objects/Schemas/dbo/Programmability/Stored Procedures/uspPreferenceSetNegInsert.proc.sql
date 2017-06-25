CREATE PROC [dbo].[uspPreferenceSetNegInsert] 
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
	
	INSERT INTO [dbo].[PreferenceSetNeg] ([PreferenceSetNegID], [NegID], [Percentage], [PreferenceSetID],[NegotiationName],[StatusID], [Deleted], [DeletedBy], [DeletedOn])
	SELECT                                @PreferenceSetNegID , @NegID , @Percentage , @PreferenceSetID ,@NegotiationName ,@StatusID , @Deleted , @DeletedBy , @DeletedOn
	
	-- Begin Return Select <- do not remove
	SELECT [PreferenceSetNegID], [NegID], [Percentage], [PreferenceSetID],[NegotiationName],[StatusID], [Deleted], [DeletedBy], [DeletedOn]
	FROM   [dbo].[PreferenceSetNeg]
	WHERE  [PreferenceSetNegID] = @PreferenceSetNegID
	-- End Return Select <- do not remove
               
	COMMIT