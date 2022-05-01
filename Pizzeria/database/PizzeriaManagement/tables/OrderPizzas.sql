CREATE TABLE [dbo].[OrderPizzas]
(
	[Id]			INT IDENTITY PRIMARY KEY NOT NULL,
	[OrderId]		INT						 NOT NULL,
	[PizzaId]		INT						 NOT NULL,
	[TotalPrice]	DECIMAL					 NOT NULL,

)
