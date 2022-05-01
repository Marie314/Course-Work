CREATE TABLE [dbo].[PizzaToppings]
(
	[Id]		INT IDENTITY PRIMARY KEY NOT NULL,
	[PizzaId]	INT						 NOT NULL,
	[ToppingId] INT						 NOT NULL,
)
