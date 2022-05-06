ALTER TABLE [dbo].[OrderItems]
ADD CONSTRAINT [FK_Pizza_OrderItems] FOREIGN KEY ([PizzaId]) 
	REFERENCES [dbo].[Pizzas] ([Id])
