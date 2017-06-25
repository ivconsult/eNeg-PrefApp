CREATE PROCEDURE [dbo].[uspHistoryDelete]
	  @SN uniqueidentifier
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[History]
	WHERE  [SN] = @SN

	COMMIT