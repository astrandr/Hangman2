IF db_id('Hangman') is NULL
BEGIN
print 'db does not exist';
CREATE DATABASE Hangman;
END


IF OBJECT_ID (N'Hangman..Players', N'U') IS NULL 
BEGIN
CREATE Table Hangman..Players
(
	PlayerID		int	NOT NULL IDENTITY(1,1) PRIMARY KEY,
	UserName		varchar(20) NOT NULL UNIQUE,
	UserPassword	varchar(20)	NOT NULL
);
END

IF OBJECT_ID (N'Hangman..Categories', N'U') IS NULL 
BEGIN
CREATE Table Hangman..Categories
(
	CategoryID		int	NOT NULL PRIMARY KEY,
	CategoryName	varchar(20) NOT NULL UNIQUE,
);
END

IF OBJECT_ID (N'Hangman..Words', N'U') IS NULL 
BEGIN
CREATE Table Hangman..Words
(
	WordID		int	NOT NULL IDENTITY(1,1) PRIMARY KEY,
	CategoryID	int NOT NULL,
	WordText	varchar(max) NOT NULL,
	WordDescription	varchar(max) NULL,
);
END

IF OBJECT_ID (N'Hangman..Games', N'U') IS NULL 
BEGIN
CREATE Table Hangman..Games
(
	GameID		int	NOT NULL IDENTITY(1,1),
	PlayerID	int NOT NULL,
	WordID		int NOT NULL,
	CorrectChars	int NULL,
	WrongChars	int NULL,
	Success		bit
);
END

IF (OBJECT_ID('Hangman..FK_WordsCategories', 'F') IS NULL)
BEGIN
ALTER TABLE Hangman..Words WITH CHECK ADD CONSTRAINT FK_WordsCategories FOREIGN KEY (CategoryID) REFERENCES Hangman..Categories (CategoryID)
END

IF (OBJECT_ID('Hangman..FK_GamesPlayer', 'F') IS NULL)
BEGIN
ALTER TABLE Hangman..Games WITH CHECK ADD CONSTRAINT FK_GamesPlayer FOREIGN KEY (PlayerID) REFERENCES Hangman..Players (PlayerID)
END

IF (OBJECT_ID('Hangman..FK_GamesWord', 'F') IS NULL)
BEGIN
ALTER TABLE Hangman..Games WITH CHECK ADD CONSTRAINT FK_GamesWord FOREIGN KEY (WordID) REFERENCES Hangman..Words (WordID)
END