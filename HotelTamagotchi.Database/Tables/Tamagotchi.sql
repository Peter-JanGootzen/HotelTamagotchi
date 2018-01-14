CREATE TABLE [dbo].[Tamagotchi]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[HotelRoomId] INT,
	[OwnerUsername] VARCHAR(20) NOT NULL,
	[Name] VARCHAR(10) NOT NULL,
	[Pennies] INT NOT NULL,
	[Age] INT NOT NULL,
	[Level] INT NOT NULL,
	[Health] TINYINT NOT NULL,
	[Boredom] TINYINT NOT NULL,
	[Alive] BIT NOT NULL,

	CONSTRAINT FK_Tamagotchi_HotelRoom FOREIGN KEY ([HotelRoomId]) REFERENCES [HotelRoom] ([Id])
				ON UPDATE CASCADE
				ON DELETE SET NULL,

	CONSTRAINT FK_Tamgotchi_OwnerUsername FOREIGN KEY ([OwnerUsername]) REFERENCES [User] ([Username])
				ON UPDATE CASCADE
				ON DELETE CASCADE,
)
