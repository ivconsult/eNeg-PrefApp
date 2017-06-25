CREATE PROC [dbo].[uspIssueTypeInsert] 
    @IssueTypeID uniqueidentifier,
    @IssueTypeName nvarchar(300)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[IssueType] ([IssueTypeID], [IssueTypeName])
	SELECT @IssueTypeID, @IssueTypeName
	
	-- Begin Return Select <- do not remove
	SELECT [IssueTypeID], [IssueTypeName]
	FROM   [dbo].[IssueType]
	WHERE  [IssueTypeID] = @IssueTypeID
	-- End Return Select <- do not remove
               
	COMMIT