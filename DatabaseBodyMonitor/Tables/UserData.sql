CREATE TABLE [dbo].[UserData] (
    [Id]             INT           NOT NULL,
    [UserBirthday]   DATETIME      NULL,
    [UserHeight]     INT           NULL,
    [UserName]       NVARCHAR (50) NULL,
    [UserMail]       NVARCHAR (50) NULL,
    [UserGender]     CHAR (1)      NULL,
    [AccountCreated] DATETIME      NULL,
    [SecretQuestion] NVARCHAR (50) NULL,
    [SecretAnswer]   NVARCHAR (50) NULL,
    CONSTRAINT [PK_UserData] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_UserData_Users] FOREIGN KEY ([Id]) REFERENCES [dbo].[Users] ([UserId])
);

