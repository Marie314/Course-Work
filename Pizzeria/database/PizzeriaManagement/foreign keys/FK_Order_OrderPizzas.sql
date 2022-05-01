ALTER TABLE [dbo].[OrderPizzas]
ADD CONSTRAINT [FK_Order_OrderPizzas] FOREIGN KEY ([OrderId]) 
	REFERENCES [dbo].[Orders] ([Id])
