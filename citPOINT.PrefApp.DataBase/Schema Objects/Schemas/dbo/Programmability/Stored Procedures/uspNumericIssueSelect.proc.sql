
CREATE PROC [dbo].[uspNumericIssueSelect] 
    @NumericIssueID UNIQUEIDENTIFIER
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [NumericIssueID], [IssueID], [MinimumValue], [MaximumValue], [OptimumValueStart], [OptimumValueEnd], [MinimumOperator], [MaximumOperator], [Unit] , [Deleted], [DeletedBy], [DeletedOn]
	FROM   [dbo].[NumericIssue] 
	WHERE  ([NumericIssueID] = @NumericIssueID OR @NumericIssueID IS NULL) 

	COMMIT