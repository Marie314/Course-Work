ALTER TABLE [dbo].[OrderPizzas]
ADD CONSTRAINT [FK_Pizza_OrderPizzas] FOREIGN KEY ([PizzaId]) 
	REFERENCES [dbo].[Pizzas] ([Id])
