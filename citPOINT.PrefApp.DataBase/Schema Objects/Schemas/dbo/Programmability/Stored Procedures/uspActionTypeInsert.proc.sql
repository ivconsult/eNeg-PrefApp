CREATE PROCEDURE [dbo].[uspActionTypeInsert]
	@ActionTypeID uniqueidentifier,
    @ActionName nvarchar(50)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[ActionType] ([ActionTypeID], [ActionName])
	SELECT @ActionTypeID, @ActionName
	
	-- Begin Return Select <- do not remove
	SELECT [ActionTypeID], [ActionName]
	FROM   [dbo].[ActionType]
	WHERE  [ActionTypeID] = @ActionTypeID
	-- End Return Select <- do not remove
               
	COMMIT