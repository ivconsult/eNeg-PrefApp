CREATE PROCEDURE [dbo].[uspHistorySelect]
	@SN UNIQUEIDENTIFIER
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [SN], [TableName], [ActionTypeID], [OldValue], [NewValue], [DoneBy], [ActionDate] 
	FROM   [dbo].[History] 
	WHERE  ([SN] = @SN OR @SN IS NULL) 

	COMMIT
