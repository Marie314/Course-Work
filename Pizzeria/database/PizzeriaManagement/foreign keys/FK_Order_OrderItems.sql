ALTER TABLE [dbo].[OrderItems]
ADD CONSTRAINT [FK_Order_OrderItems] FOREIGN KEY ([OrderId]) 
	REFERENCES [dbo].[Orders] ([Id])
