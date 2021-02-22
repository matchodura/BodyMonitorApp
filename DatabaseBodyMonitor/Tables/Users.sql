CREATE TABLE [dbo].[Users] (
    [UserId]       INT            IDENTITY (1, 1) NOT NULL,
    [UserLogin]    NCHAR (10)     NULL,  
    [Hash]         NVARCHAR (MAX) NULL,
    [Salt]         NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([UserId] ASC),
    UNIQUE NONCLUSTERED ([UserLogin] ASC)
);

