Select  Products.ProductName, Products.UnitPrice, Categories.CategoryName From Products 
join Categories on Products.CategoryID= Categories.CategoryID
ORDER BY Categories.CategoryName, Products.ProductName ;




