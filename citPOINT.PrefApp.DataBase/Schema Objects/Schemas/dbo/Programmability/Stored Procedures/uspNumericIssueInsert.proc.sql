
CREATE PROC [dbo].[uspNumericIssueInsert] 
    @NumericIssueID uniqueidentifier,
    @IssueID uniqueidentifier,
    @MinimumValue  decimal(18, 2) ,
    @MaximumValue  decimal(18, 2) ,
    @OptimumValueStart  decimal(18, 2) ,
    @OptimumValueEnd  decimal(18, 2) ,
    @MinimumOperator tinyint,
    @MaximumOperator tinyint,
    @Unit nvarchar(100),
    @Deleted bit,
    @DeletedBy uniqueidentifier,
    @DeletedOn datetime
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[NumericIssue] ([NumericIssueID], [IssueID], [MinimumValue], [MaximumValue], [OptimumValueStart], [OptimumValueEnd], [MinimumOperator], [MaximumOperator], [Unit], [Deleted], [DeletedBy], [DeletedOn])
	SELECT @NumericIssueID, @IssueID, @MinimumValue, @MaximumValue, @OptimumValueStart, @OptimumValueEnd, @MinimumOperator, @MaximumOperator, @Unit, @Deleted, @DeletedBy, @DeletedOn  
	
	-- Begin Return Select <- do not remove
	SELECT [NumericIssueID], [IssueID], [MinimumValue], [MaximumValue], [OptimumValueStart], [OptimumValueEnd], [MinimumOperator], [MaximumOperator], [Unit], [Deleted], [DeletedBy], [DeletedOn]
	FROM   [dbo].[NumericIssue]
	WHERE  [NumericIssueID] = @NumericIssueID
	-- End Return Select <- do not remove
               
	COMMIT