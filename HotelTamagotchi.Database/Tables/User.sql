﻿CREATE TABLE [dbo].[User]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[Username] VARCHAR(20) NOT NULL UNIQUE,
	[Password] VARCHAR(20) NOT NULL,
	[Role] INT NOT NULL,
)
