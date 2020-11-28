CREATE TABLE [dbo].[UserBodyValues]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [BodyId] INT NOT NULL,
    [DateAdded] DATETIME NOT NULL, 
    [Weight] DECIMAL(5, 2) NOT NULL, 
    [Neck] DECIMAL(5, 1) NOT NULL, 
    [Chest] DECIMAL(5, 1) NOT NULL, 
    [Biceps] DECIMAL(5, 1) NOT NULL, 
    [Stomach] DECIMAL(5, 1) NOT NULL, 
    CONSTRAINT [FK_UserBodyValues_ToTable] FOREIGN KEY ([BodyId]) REFERENCES [UserData]([Id]) 
)
