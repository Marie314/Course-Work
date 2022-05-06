CREATE TABLE [dbo].[OrderItems]
(
	[Id]			INT IDENTITY PRIMARY KEY NOT NULL,
	[OrderId]		INT						 NOT NULL,
	[PizzaId]		INT						 NOT NULL,
	[Price]			DECIMAL					 NOT NULL,

)
