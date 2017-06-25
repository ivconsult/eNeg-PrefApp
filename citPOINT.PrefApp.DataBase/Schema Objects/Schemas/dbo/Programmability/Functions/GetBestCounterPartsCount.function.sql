CREATE FUNCTION [dbo].[GetBestCounterPartsCount]
(
	@KeyWord NVARCHAR(300),
	@CurrentNegotiationID UniqueIdentifier
)
RETURNS INT
AS
BEGIN
	
RETURN ISNULL((SELECT SUM(CounterPartsCount) / COUNT(*) AS [CounterPart Average]
                FROM   dbo.HighValuesNegs 
               WHERE  (PreferenceSetName LIKE '%' + @KeyWord + '%' 
                       OR NegotiationName LIKE '%' + @KeyWord + '%') AND 
					   NegID!=@CurrentNegotiationID), 0)  
END