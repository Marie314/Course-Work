ALTER TABLE [dbo].[Pizzas]
ADD CONSTRAINT [FK_Size_Pizza] FOREIGN KEY ([SizeId]) 
	REFERENCES [dbo].[Sizes] ([Id])
