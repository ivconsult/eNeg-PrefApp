CREATE PROCEDURE [dbo].[uspActionTypeSelect]
	@ActionTypeID UNIQUEIDENTIFIER
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [ActionTypeID], [ActionName] 
	FROM   [dbo].[ActionType] 
	WHERE  ([ActionTypeID] = @ActionTypeID OR @ActionTypeID IS NULL) 

	COMMIT
