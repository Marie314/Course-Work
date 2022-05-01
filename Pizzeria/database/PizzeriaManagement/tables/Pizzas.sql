CREATE TABLE [dbo].[Pizzas]
(
	[Id]			INT IDENTITY PRIMARY KEY NOT NULL,
	[Name]			NVARCHAR (256)			 NOT NULL,
	[SizeId]		INT						 NOT NULL,
	[Description]	NVARCHAR (256)			 NOT NULL,
	[ImagePath]		NVARCHAR (256)			 NOT NULL,
	[TotalPrice]	DECIMAL					 NOT NULL,
)
