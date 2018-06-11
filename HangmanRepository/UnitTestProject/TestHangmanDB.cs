using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HangmanRepository;
using System.Collections.Generic;

namespace UnitTestProject
{
    [TestClass]
    public class TestHangmanDB
    {
        [TestMethod]
        public void TestCategoriesRead()
        {
            IHangmanRepository repository = HangmanRepositoryFactory.CreateRepository();
            int[] categories = repository.GetCategories();

            Assert.IsNotNull(categories);
            Assert.AreEqual(2, categories.Length);
            Assert.AreEqual("Cities", repository.GetCategoryByID(categories[0]));
            Assert.AreEqual("States", repository.GetCategoryByID(categories[1]));
        }

        [TestMethod]
        public void TestCategoryWords()
        {
            IHangmanRepository repository = HangmanRepositoryFactory.CreateRepository();
            int[] words = repository.GetWords(2);

            Assert.IsNotNull(words);
            Assert.AreEqual(11, words.Length);
            Assert.AreEqual(13, words[0]);
            Assert.AreEqual(14, words[1]);
        }

        [TestMethod]
        public void TestWordByID()
        {
            IHangmanRepository repository = HangmanRepositoryFactory.CreateRepository();
            string word = repository.GetWordByID(13)[0];

            Assert.IsNotNull(word);
            Assert.AreEqual("Seattle", word);
        }


        [TestMethod]
        public void TestAddPlayer()
        {
            IHangmanRepository repository = HangmanRepositoryFactory.CreateRepository();
            int id = repository.GetPlayerID("player1");
            if (id != 0)
            {
                repository.RemovePlayerByID(id);
            }
            bool firstTimeAdded = repository.AddPlayer("player1", "password");
            bool secondTimeAdded = repository.AddPlayer("player1", "password");

            Assert.IsTrue(firstTimeAdded = id > 0 ? true : false);
            Assert.IsFalse(secondTimeAdded);
        }

        [TestMethod]
        public void TestGetPlayerID()
        {
            IHangmanRepository repository = HangmanRepositoryFactory.CreateRepository();
            int id = repository.GetPlayerID("player1");
        }

        [TestMethod]
        public void TestVerifyUser()
        {
            IHangmanRepository repository = HangmanRepositoryFactory.CreateRepository();
            int id1 = repository.GetPlayerID("player1");
            if (id1 == 0)
            {
                //Add player
                repository.AddPlayer("player1", "password");
                id1 = repository.GetPlayerID("player1");
            }
            Assert.IsTrue(id1 > 0);
            bool userVerified = repository.VerifyUser(id1, "player1", "password");

            Assert.IsTrue(userVerified);

            bool userVerificationFailedForName = !repository.VerifyUser(id1, "player", "password");
            Assert.IsTrue(userVerificationFailedForName);

            bool userVerificationFailedForPassword = !repository.VerifyUser(id1, "player1", "password2");
            Assert.IsTrue(userVerificationFailedForPassword);
        }

        [TestMethod]
        public void TestRemovePlayerByID()
        {
            IHangmanRepository repository = HangmanRepositoryFactory.CreateRepository();
            int playerID = repository.GetPlayerID("player1");
            GamesResult gamesResult;

            if (playerID == 0)
            {
                //Add player
                repository.AddPlayer("player1", "password");
                playerID = repository.GetPlayerID("player1");
            }

            gamesResult = repository.GetGamesResultForPlayer(playerID);
            if (gamesResult.NumberOfGames == 0)
            {
                int wordID = repository.GetWords(1)[0];
                repository.RecordGame(playerID, wordID, 3, 3, true);
                gamesResult = repository.GetGamesResultForPlayer(playerID);
            }
            Assert.IsTrue(gamesResult.NumberOfGames > 0);
            Assert.IsTrue(gamesResult.NumberOfSuccess > 0);

            Assert.IsTrue(playerID > 0);

            repository.RemovePlayerByID(playerID);

            int id2 = repository.GetPlayerID("player1");
            gamesResult = repository.GetGamesResultForPlayer(playerID);
            Assert.IsTrue(id2 == 0);
            Assert.IsTrue(gamesResult.NumberOfGames == 0);
        }

        [TestMethod]
        public void TestUpdatePlayerByID()
        {
            IHangmanRepository repository = HangmanRepositoryFactory.CreateRepository();
            int id1 = repository.GetPlayerID("player1");

            if (id1 == 0)
            {
                //Add player
                repository.AddPlayer("player1", "password");
                id1 = repository.GetPlayerID("player1");
            }
            Assert.IsTrue(id1 > 0);

            repository.UpdatePlayerByID(id1, "player12", "password12");

            int id2 = repository.GetPlayerID("player1");
            Assert.IsTrue(id2 == 0);

            int id3 = repository.GetPlayerID("player12");
            Assert.IsTrue(id3 == id1);

            repository.UpdatePlayerByID(id1, "player1", "password");

            int id4 = repository.GetPlayerID("player1");
            Assert.IsTrue(id4 == id1);
        }

        [TestMethod]
        public void TestRecordGame()
        {
            IHangmanRepository repository = HangmanRepositoryFactory.CreateRepository();
            int playerID = repository.GetPlayerID("player1");

            if (playerID == 0)
            {
                //Add player
                repository.AddPlayer("player1", "password");
                playerID = repository.GetPlayerID("player1");
            }


            int wordID = repository.GetWords(1)[0];

            repository.RecordGame(playerID, wordID, 3, 3, true);

            GamesResult gamesResult = repository.GetGamesResultForPlayer(playerID);
            Assert.IsTrue(gamesResult.NumberOfGames > 0);
            Assert.IsTrue(gamesResult.NumberOfSuccess > 0);
        }
    }
}
