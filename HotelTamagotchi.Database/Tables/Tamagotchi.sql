CREATE TABLE [dbo].[Tamagotchi]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[HotelRoomId] INT,
	[Name] VARCHAR(10) NOT NULL,
	[Pennies] INT NOT NULL,
	[Age] INT NOT NULL,
	[Level] INT NOT NULL,
	[Health] TINYINT NOT NULL,
	[Boredom] TINYINT NOT NULL,
	[Alive] BIT NOT NULL,

	CONSTRAINT FK_Tamagotchi_HotelRoomId FOREIGN KEY ([HotelRoomId]) REFERENCES [HotelRoom] ([Id])
				ON UPDATE CASCADE
				ON DELETE SET NULL,
)
