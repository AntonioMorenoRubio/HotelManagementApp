CREATE TABLE [dbo].[Reservations]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
    [ClientId] INT NOT NULL, 
    [RoomId] INT NOT NULL,
    [StartDate] DATE NOT NULL, 
    [EndDate] DATE NOT NULL, 
    [ClientHasCheckedIn] BIT NOT NULL DEFAULT 0, 
    [TotalCost] MONEY NOT NULL, 
    CONSTRAINT [FK_Reservations_Clients] FOREIGN KEY ([ClientId]) REFERENCES Clients(Id), 
    CONSTRAINT [FK_Reservations_Rooms] FOREIGN KEY ([RoomId]) REFERENCES Rooms(Id)
)
