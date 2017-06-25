CREATE PROCEDURE [dbo].[uspHistoryUpdate]
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

	UPDATE [dbo].[History]
	SET    [SN] = @SN, [TableName] = @TableName, [ActionTypeID] = @ActionTypeID, [OldValue] = @OldValue, [NewValue] = @NewValue, [DoneBy] = @DoneBy, [ActionDate] = @ActionDate
	WHERE  [SN] = @SN
	
	-- Begin Return Select <- do not remove
	SELECT [SN], [TableName], [ActionTypeID], [OldValue], [NewValue], [DoneBy], [ActionDate]
	FROM   [dbo].[History]
	WHERE  [SN] = @SN	
	-- End Return Select <- do not remove

	COMMIT TRAN
