IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'BlogApp')
BEGIN
	CREATE DATABASE [BlogApp];
END;

GO
USE [BlogApp];
GO

CREATE TABLE [UserRole](
	[Id] INT IDENTITY(1,1) PRIMARY KEY,
	[Role] VARCHAR(100) NOT NULL
);

INSERT INTO [UserRole] VALUES ('Admin');
INSERT INTO [UserRole] VALUES ('Default');

CREATE TABLE [User](
	[Id] INT IDENTITY(1,1) PRIMARY KEY,
	[IdRole] INT,
	[Username] VARCHAR(100) NOT NULL,
	[Email] VARCHAR(254) NOT NULL,
	[Password] VARCHAR(255) NOT NULL,
	[ProfileImageName] VARCHAR(255) NOT NULL,

	FOREIGN KEY ([IdRole]) REFERENCES [UserRole]([Id])
);

CREATE TABLE [PostCategory](
	[Id] INT IDENTITY(1,1) PRIMARY KEY,
	[Name] VARCHAR(100) NOT NULL
);

INSERT INTO [PostCategory] VALUES ('Technology');
INSERT INTO [PostCategory] VALUES ('Programming');
INSERT INTO [PostCategory] VALUES ('Productivity');
INSERT INTO [PostCategory] VALUES ('SelfImprovement');

CREATE TABLE [Post](
	[Id] INT IDENTITY(1,1) PRIMARY KEY,
	[IdUserAuthor] INT,
	[IdCategory] INT,
	[Title]	VARCHAR(100) NOT NULL,
	[Content] VARCHAR(1000) NOT NULL,
	[PostImageName] VARCHAR(256) NOT NULL,
	[CreationDate] DATETIME NOT NULL,
	[PublishedDate] DATETIME NULL,
	[ViewCount] INT DEFAULT 0 NOT NULL,

	FOREIGN KEY ([IdUserAuthor]) REFERENCES [User]([Id]),
	FOREIGN KEY ([IdCategory]) REFERENCES [PostCategory]([Id])
);

CREATE TABLE [PostReview](
	[Id] INT IDENTITY(1,1) PRIMARY KEY,
	[IdUserAuthor] INT,
	[IdUserReviewer] INT,
	[Status] INT NOT NULL,
	[Feedback] VARCHAR (255) NULL,
	[ReviewDate] DATETIME NULL,

	FOREIGN KEY ([IdUserAuthor]) REFERENCES [User]([Id]),
	FOREIGN KEY ([IdUserReviewer]) REFERENCES [User]([Id])
);

CREATE TABLE [PostLikes](
	[Id] INT IDENTITY(1,1) PRIMARY KEY,
	[IdPost] INT,
	[IdUser] INT,
	[LikeDate] DATETIME NOT NULL,

	FOREIGN KEY ([IdPost]) REFERENCES [Post]([Id]),
	FOREIGN KEY ([IdUser]) REFERENCES [User]([Id])
);

CREATE TABLE [PostComments](
	[Id] INT IDENTITY(1,1) PRIMARY KEY,
	[IdPost] INT,
	[IdUser] INT,
	[Comment] VARCHAR (500) NOT NULL,
	[CommentDate] DATETIME NOT NULL,

	FOREIGN KEY ([IdPost]) REFERENCES [Post]([Id]),
	FOREIGN KEY ([IdUser]) REFERENCES [User]([Id])
);

CREATE TABLE [SavedPosts](
	[Id] INT IDENTITY(1,1) PRIMARY KEY,
	[IdPost] INT,
	[IdUser] INT,
	[SaveDate] DATETIME NOT NULL,

	FOREIGN KEY ([IdPost]) REFERENCES [Post]([Id]),
	FOREIGN KEY ([IdUser]) REFERENCES [User]([Id])
);