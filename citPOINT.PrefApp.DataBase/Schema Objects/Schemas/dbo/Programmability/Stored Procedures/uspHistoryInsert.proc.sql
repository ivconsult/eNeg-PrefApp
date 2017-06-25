CREATE PROCEDURE [dbo].[uspHistoryInsert]
	@SN uniqueidentifier,
    @TableName nvarchar(50),
    @ActionTypeID uniqueidentifier,
    @OldValue xml,
    @NewValue xml,
    @DoneBy uniqueidentifier,
    @ActionDate datetime
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[History] ([SN], [TableName], [ActionTypeID], [OldValue], [NewValue], [DoneBy], [ActionDate])
	SELECT @SN, @TableName, @ActionTypeID, @OldValue, @NewValue, @DoneBy, @ActionDate
	
	-- Begin Return Select <- do not remove
	SELECT [SN], [TableName], [ActionTypeID], [OldValue], [NewValue], [DoneBy], [ActionDate]
	FROM   [dbo].[History]
	WHERE  [SN] = @SN
	-- End Return Select <- do not remove
               
	COMMIT