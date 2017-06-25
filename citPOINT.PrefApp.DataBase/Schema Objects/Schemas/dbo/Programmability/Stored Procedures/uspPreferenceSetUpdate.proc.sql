CREATE PROC [dbo].[uspPreferenceSetUpdate] 
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
  SET nocount ON 
  SET xact_abort ON 

  BEGIN TRAN 

UPDATE [DBO].[PreferenceSet]
SET    [PreferenceSetID]     = @PreferenceSetID    ,
       [PreferenceSetName]   = @PreferenceSetName  ,
       [Checkvariation]      = @Checkvariation     ,
       [VariationValue]      = @VariationValue     ,
       [MaxPercentage]       = @MaxPercentage      ,
       [UserID]              = @UserID             ,
       [MainPreferenceSetID] = @MainPreferenceSetID,
       [Deleted]             = @Deleted            ,
       [DeletedBy]           = @DeletedBy          ,
       [DeletedOn]           = @DeletedOn
WHERE  [PreferenceSetID]     = @PreferenceSetID

  -- Begin Return Select <- do not remove 
  SELECT [PreferenceSetID], 
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
  COMMIT TRAN 