CREATE PROCEDURE [dbo].[uspIssueSelectStatisticals]
	@UserID UNIQUEIDENTIFIER
AS
BEGIN
	SELECT   TOP (10)IssueName, COUNT(*) AS [TimesUsed],
	 		 ROW_NUMBER() over(order by COUNT(*) desc) as Rank
	FROM     dbo.Issue
	WHERE    (Deleted = 0) --and [DeletedBy] != @UserID 
	GROUP BY IssueName
	ORDER BY [TimesUsed] desc
END
