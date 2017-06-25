CREATE PROC [dbo].[uspIssueUpdate] 
    @IssueID uniqueidentifier,
    @IssueName nvarchar(300),
    @PreferenceSetID uniqueidentifier,
    @IssueTypeID uniqueidentifier,
    @IssueWeight  decimal(18, 2) ,
    @Deleted bit,
    @DeletedBy uniqueidentifier,
    @DeletedOn datetime
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[Issue]
	SET    [IssueID] = @IssueID, [IssueName] = @IssueName, [PreferenceSetID] = @PreferenceSetID, [IssueTypeID] = @IssueTypeID, [IssueWeight] = @IssueWeight, [Deleted] = @Deleted, [DeletedBy] = @DeletedBy, [DeletedOn] = @DeletedOn
	WHERE  [IssueID] = @IssueID
	
	-- Begin Return Select <- do not remove
	SELECT [IssueID], [IssueName], [PreferenceSetID], [IssueTypeID], [IssueWeight], [Deleted], [DeletedBy], [DeletedOn]
	FROM   [dbo].[Issue]
	WHERE  [IssueID] = @IssueID	
	-- End Return Select <- do not remove

	COMMIT TRAN