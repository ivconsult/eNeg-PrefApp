
CREATE PROC [dbo].[uspIssueTypeUpdate] 
    @IssueTypeID uniqueidentifier,
    @IssueTypeName nvarchar(300)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[IssueType]
	SET    [IssueTypeID] = @IssueTypeID, [IssueTypeName] = @IssueTypeName
	WHERE  [IssueTypeID] = @IssueTypeID
	
	-- Begin Return Select <- do not remove
	SELECT [IssueTypeID], [IssueTypeName]
	FROM   [dbo].[IssueType]
	WHERE  [IssueTypeID] = @IssueTypeID	
	-- End Return Select <- do not remove

	COMMIT TRAN