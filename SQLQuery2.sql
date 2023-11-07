SELECT Customers.CustomerID,  Count(Orders.OrderID) As TotalOrders 
FROM Customers 
Join Orders ON Customers.CustomerID = Orders.CustomerID
Group By Customers.CustomerID
ORDER BY totalOrders DESC;



