CREATE TABLE [dbo].[Orders]
(
	[Id]		  INT IDENTITY PRIMARY KEY	NOT NULL,
	[UserId]	  INT						NOT NULL,
	[Address]	  NVARCHAR (256)			NOT NULL,
	[OrderDate]	  DATETIMEOFFSET			NOT NULL,
	[TotalPrice]  DECIMAL					NOT NULL,
	[Description] NVARCHAR (256)			NOT NULL,
	[UserName]	  NVARCHAR (256)		    NOT NULL,
)