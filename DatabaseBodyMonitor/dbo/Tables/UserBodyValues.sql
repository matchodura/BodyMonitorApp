CREATE TABLE [dbo].[UserBodyValues] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [UserId]    INT            NULL,
    [PostId]    INT            NULL,
    [DateAdded] DATETIME       NULL,
    [Weight]    DECIMAL (5, 2) NULL,
    [Neck]      DECIMAL (5, 1) NULL,
    [Chest]     DECIMAL (5, 1) NULL,
    [Stomach]   DECIMAL (5, 1) NULL,
    [Waist]     DECIMAL (5, 1) NULL,
    [Hips]      DECIMAL (5, 1) NULL,
    [Thigh]     DECIMAL (5, 1) NULL,
    [Calf]      DECIMAL (5, 1) NULL,
    [Biceps]    DECIMAL (5, 1) NULL,
    [Note]      TEXT           NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

