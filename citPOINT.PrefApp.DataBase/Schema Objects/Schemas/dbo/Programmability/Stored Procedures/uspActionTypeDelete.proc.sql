CREATE PROCEDURE [dbo].[uspActionTypeDelete]
	@ActionTypeID uniqueidentifier
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[ActionType]
	WHERE  [ActionTypeID] = @ActionTypeID

	COMMIT
