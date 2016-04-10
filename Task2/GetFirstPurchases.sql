SELECT ProductID
, SUM(IsFirst) AS FirstPurchaseTimes
FROM
(
	SELECT ProductID
	, CASE WHEN EXISTS
	(
		SELECT TOP 1 1 FROM Sales ss
		WHERE s.DateTime > ss.DateTime AND s.CustomerID = ss.CustomerID 
	) 
		THEN 0
		ELSE 1
	END AS IsFirst
	FROM Sales s
)
data
GROUP BY data.ProductID