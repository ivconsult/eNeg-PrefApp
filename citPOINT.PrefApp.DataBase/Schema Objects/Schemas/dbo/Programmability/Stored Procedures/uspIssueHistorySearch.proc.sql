CREATE PROCEDURE [dbo].[uspIssueHistorySearch]
	@KeyWord nvarchar(300),
	@CurrentNegotiationID UniqueIdentifier
AS
BEGIN
	
	SELECT TOP(10)IssueName, COUNT(*) AS [TimesUsed], AVG(IssueWeight) AS [AverageScore],
	       ROW_NUMBER() OVER(ORDER BY COUNT(*) DESC) AS Rank
	FROM dbo.IssueHistoryView
	WHERE (PreferenceSetName like '%' + @KeyWord +'%' or 
	      NegotiationName like '%' + @KeyWord +'%') AND
		  NegID!=@CurrentNegotiationID
	GROUP BY IssueName

	union

	SELECT @KeyWord, COUNT(distinct NegotiationName) /* Number of matched negotiations */,
		   dbo.GetBestCounterPartsCount(@KeyWord,@CurrentNegotiationID)/* Prefered Number of counter parts user should negotiation with */, 0
	FROM dbo.IssueHistoryView
	WHERE (PreferenceSetName like '%' + @KeyWord +'%' or 
		  NegotiationName like '%' + @KeyWord +'%') AND
		  NegID!=@CurrentNegotiationID

		  
	ORDER BY [Rank]
	--ORDER BY [Times Used] desc
END