CREATE TABLE [dbo].[Users]
(
	[UserId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UserLogin] NCHAR(10) NOT NULL, 
    [UserPassword] NCHAR(10) NOT NULL
)
