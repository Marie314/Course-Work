ALTER TABLE [dbo].[PizzaToppings]
ADD CONSTRAINT [FK_Topping_PizzaToppings] FOREIGN KEY ([ToppingId]) 
	REFERENCES [dbo].[Toppings] ([Id])
