CREATE TABLE [dbo].[OrderItems]
(
	[Id]			INT IDENTITY PRIMARY KEY NOT NULL,
	[OrderId]		INT						 NOT NULL,
	[PizzaId]		INT,
	[Price]			DECIMAL					 NOT NULL,
	[Quantity]	    INT						 NOT NULL,
	[PizzaName]	    NVARCHAR (256)			 NOT NULL,
)
