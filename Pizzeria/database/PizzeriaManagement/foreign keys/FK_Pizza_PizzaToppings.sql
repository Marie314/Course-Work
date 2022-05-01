ALTER TABLE [dbo].[PizzaToppings]
ADD CONSTRAINT [FK_Pizza_PizzaToppings] FOREIGN KEY ([PizzaId]) 
	REFERENCES [dbo].[Pizzas] ([Id])
