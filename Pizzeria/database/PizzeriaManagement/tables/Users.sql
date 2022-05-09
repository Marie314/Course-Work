-- Mocked
CREATE TABLE [dbo].[Users]
(
	[Id]		INT IDENTITY PRIMARY KEY NOT NULL,
	[Name]		NVARCHAR (256)			 NOT NULL,
	[UserName]  NVARCHAR (256)			 NOT NULL,
	[Password]  NVARCHAR (256)			 NOT NULL,
	[RoleId]	INT						 NOT NULL,
)
