CREATE TRIGGER PlayersGamesDeleteTrigger 
ON Hangman..Players INSTEAD OF DELETE AS 
BEGIN
	DELETE FROM Hangman..Games WHERE PlayerID IN (SELECT deleted.PlayerID FROM deleted)
	DELETE FROM Hangman..Players WHERE PlayerID IN (SELECT deleted.PlayerID FROM deleted)
END