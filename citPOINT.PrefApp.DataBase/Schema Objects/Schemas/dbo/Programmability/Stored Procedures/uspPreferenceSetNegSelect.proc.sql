CREATE PROC [dbo].[uspPreferenceSetNegSelect] 
    @PreferenceSetNegID UNIQUEIDENTIFIER
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [PreferenceSetNegID] ,[NegID] , [Percentage], [PreferenceSetID],[NegotiationName],[StatusID],[Deleted], [DeletedBy], [DeletedOn] 
	FROM   [dbo].[PreferenceSetNeg] 
	WHERE  ([PreferenceSetNegID] = @PreferenceSetNegID OR @PreferenceSetNegID IS NULL) 

	COMMIT