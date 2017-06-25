
CREATE PROC [dbo].[uspIssueTypeSelect] 
    @IssueTypeID UNIQUEIDENTIFIER
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [IssueTypeID], [IssueTypeName] 
	FROM   [dbo].[IssueType] 
	WHERE  ([IssueTypeID] = @IssueTypeID OR @IssueTypeID IS NULL) 

	COMMIT