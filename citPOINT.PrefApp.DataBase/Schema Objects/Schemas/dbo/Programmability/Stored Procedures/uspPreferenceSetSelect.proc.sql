CREATE PROC [dbo].[uspPreferenceSetSelect] 
    @PreferenceSetID UNIQUEIDENTIFIER
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

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
	WHERE  ([PreferenceSetID] = @PreferenceSetID OR @PreferenceSetID IS NULL) 

	COMMIT