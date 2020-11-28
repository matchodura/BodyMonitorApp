CREATE TABLE [dbo].UserData
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UserId] INT NOT NULL, 
    [Age] INT NOT NULL, 
    [Height] INT NOT NULL, 
    [Name] NCHAR(10) NOT NULL, 
    [Mail] NCHAR(10) NOT NULL, 
    [Gender] CHAR NOT NULL, 
    [AccountCreated] DATETIME NOT NULL, 
    CONSTRAINT [FK_UserData_Users] FOREIGN KEY ([UserId]) REFERENCES [Users]([UserId])
)
