IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'BlogApp')
BEGIN
	CREATE DATABASE [BlogApp];
END;

GO
USE [BlogApp];
GO

CREATE TABLE [UsersRoles](
	[Id] INT IDENTITY(1,1) PRIMARY KEY,
	[Role] VARCHAR(100) NOT NULL
);

INSERT INTO [UsersRoles] VALUES ('Admin');
INSERT INTO [UsersRoles] VALUES ('Default');

CREATE TABLE [Users](
	[Id] INT IDENTITY(1,1) PRIMARY KEY,
	[IdRole] INT,
	[Username] VARCHAR(100) NOT NULL,
	[Email] VARCHAR(254) NOT NULL,
	[Password] VARCHAR(255) NOT NULL,
	[ProfileImageName] VARCHAR(255) NOT NULL,

	FOREIGN KEY ([IdRole]) REFERENCES [UsersRoles]([Id])
);

CREATE TABLE [PostsCategories](
	[Id] INT IDENTITY(1,1) PRIMARY KEY,
	[Name] VARCHAR(100) NOT NULL
);

INSERT INTO [PostsCategories] VALUES ('Technology');
INSERT INTO [PostsCategories] VALUES ('Programming');
INSERT INTO [PostsCategories] VALUES ('Productivity');
INSERT INTO [PostsCategories] VALUES ('SelfImprovement');

CREATE TABLE [Posts](
	[Id] INT IDENTITY(1,1) PRIMARY KEY,
	[IdUserAuthor] INT,
	[IdCategory] INT,
	[Title]	VARCHAR(100) NOT NULL,
	[Content] VARCHAR(1000) NOT NULL,
	[PostImageName] VARCHAR(256) NOT NULL,
	[CreationDate] DATETIME NOT NULL DEFAULT GETDATE(),
	[ViewCount] INT DEFAULT 0 NOT NULL,

	FOREIGN KEY ([IdUserAuthor]) REFERENCES [Users]([Id]),
	FOREIGN KEY ([IdCategory]) REFERENCES [PostsCategories]([Id])
);

CREATE TABLE [PostsReviews](
	[Id] INT IDENTITY(1,1) PRIMARY KEY,
	[IdPost] INT NOT NULL,
	[IdUserReviewer] INT,
	[Status] INT NOT NULL,
	[Feedback] VARCHAR (255) NULL,
	[ReviewDate] DATETIME NULL,

	FOREIGN KEY ([IdPost]) REFERENCES [Posts]([Id]),
	FOREIGN KEY ([IdUserReviewer]) REFERENCES [Users]([Id])
);

CREATE TABLE [PostsLikes](
	[Id] INT IDENTITY(1,1) PRIMARY KEY,
	[IdPost] INT,
	[IdUser] INT,
	[LikeDate] DATETIME NOT NULL DEFAULT GETDATE(),

	FOREIGN KEY ([IdPost]) REFERENCES [Posts]([Id]),
	FOREIGN KEY ([IdUser]) REFERENCES [Users]([Id])
);

CREATE TABLE [PostsComments](
	[Id] INT IDENTITY(1,1) PRIMARY KEY,
	[IdPost] INT,
	[IdUser] INT,
	[Comment] VARCHAR (500) NOT NULL,
	[CommentDate] DATETIME NOT NULL DEFAULT GETDATE(),

	FOREIGN KEY ([IdPost]) REFERENCES [Posts]([Id]),
	FOREIGN KEY ([IdUser]) REFERENCES [Users]([Id])
);

CREATE TABLE [SavedPosts](
	[Id] INT IDENTITY(1,1) PRIMARY KEY,
	[IdPost] INT NOT NULL,
	[IdUser] INT NOT NULL,
	[SaveDate] DATETIME NOT NULL DEFAULT GETDATE(),

	FOREIGN KEY ([IdPost]) REFERENCES [Posts]([Id]),
	FOREIGN KEY ([IdUser]) REFERENCES [Users]([Id])
);