CREATE PROC [dbo].[uspPreferenceSetInsert]
	@PreferenceSetID     uniqueidentifier,
	@PreferenceSetName   nvarchar(300),
	@Checkvariation      bit,
	@VariationValue      decimal(18, 2),
	@MaxPercentage	     decimal(18, 2),
	@UserID              uniqueidentifier,
	@MainPreferenceSetID uniqueidentifier,
	@Deleted             bit,
	@DeletedBy           uniqueidentifier,
	@DeletedOn           datetime 
	
	AS


	SET NOCOUNT ON
	SET XACT_ABORT ON

	BEGIN TRAN

	INSERT INTO   [dbo].[PreferenceSet]
		   (
				  [PreferenceSetID]    ,
				  [PreferenceSetName]  ,
				  [Checkvariation]     ,
				  [VariationValue]     ,
				  [MaxPercentage]      ,
				  [UserID]             ,
				  [MainPreferenceSetID],
				  [Deleted]            ,
				  [DeletedBy]          ,
				  [DeletedOn]
		   )
	SELECT @PreferenceSetID    ,
		   @PreferenceSetName  ,
		   @Checkvariation     ,
		   @VariationValue     ,
		   @MaxPercentage      ,
		   @UserID             ,
		   @MainPreferenceSetID,
		   @Deleted            ,
		   @DeletedBy          ,
		   @DeletedOn

	-- Begin Return Select <- do not remove
	SELECT	[PreferenceSetID],
			[PreferenceSetName],
			[Checkvariation],
			[VariationValue],
			[MaxPercentage],
			[UserID],
			[MainPreferenceSetID],
			[Deleted],
			[DeletedBy],
			[DeletedOn]
	FROM   [dbo].[PreferenceSet]
	WHERE  [PreferenceSetID] = @PreferenceSetID
		   -- End Return Select <- do not remove
	
	COMMIT