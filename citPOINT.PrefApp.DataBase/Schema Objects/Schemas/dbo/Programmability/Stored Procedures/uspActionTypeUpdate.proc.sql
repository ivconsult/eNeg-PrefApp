CREATE PROCEDURE [dbo].[uspActionTypeUpdate]
	@ActionTypeID uniqueidentifier,
    @ActionName nvarchar(50)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[ActionType]
	SET    [ActionTypeID] = @ActionTypeID, [ActionName] = @ActionName
	WHERE  [ActionTypeID] = @ActionTypeID
	
	-- Begin Return Select <- do not remove
	SELECT [ActionTypeID], [ActionName]
	FROM   [dbo].[ActionType]
	WHERE  [ActionTypeID] = @ActionTypeID	
	-- End Return Select <- do not remove

	COMMIT TRAN