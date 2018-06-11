using HangmanRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace HangmanWeb.Models
{
    public class Game
    {
        private int categoryID;
        private IHangmanRepository repository;
        private int wordID;
        private int playerID;
        private string GuessedWord { get; set; }
        private string Word { get; set; }

        public string Description { get; set; }
        public string DisplayWord { get; set; }
        public string WrongChars { get; set; }
        public int WrongCharsCount { get; set; }
        public string Status { get; set; }

        private static List<KeyValuePair<int, string>> categories = null;

        public static List<KeyValuePair<int, string>> GetCategories()
        {
            if (categories == null)
            {
                categories = new List<KeyValuePair<int, string>>();
                IHangmanRepository repository = HangmanRepositoryFactory.CreateRepository();
                int[] categoryIDs = repository.GetCategories();
                foreach(int id in categoryIDs)
                {
                    categories.Add(new KeyValuePair<int, string>(id, repository.GetCategoryByID(id)));
                }
            }

            return categories;

        }

        public Game(int categoryID, string username)
        {
            this.categoryID = categoryID;
            repository = HangmanRepositoryFactory.CreateRepository();
            int[] wordIDs = repository.GetWords(categoryID);
            this.playerID = repository.GetPlayerID(username);

            Random rnd = new Random();
            int n = rnd.Next(wordIDs.Length - 1);

            wordID = wordIDs[n];

            string[] wordInfo = repository.GetWordByID(wordID);

            Word = wordInfo[0];
            Description = wordInfo[1];

            StringBuilder sb = new StringBuilder();
            sb.Append(Word[0]);
            for (int i = 1; i < Word.Length - 1; i++) sb.Append("_");
            sb.Append(Word[Word.Length - 1]);

            GuessedWord = sb.ToString();

            sb.Clear(); sb.Append(GuessedWord[0]);

            for (int i = 1; i < GuessedWord.Length; i++)
            {
                sb.Append(" " + GuessedWord[i]);
            }

            DisplayWord = sb.ToString();

            WrongChars = "";

            WrongCharsCount = 0;
            Status = "";

        }

        public void ProcessChar(char c)
        {
            bool foundChar = false;
            StringBuilder sb = new StringBuilder();
            sb.Append(GuessedWord[0]);
            for (int i = 1; i < GuessedWord.Length - 1; i++)
            {
                if (GuessedWord[i] == '_')
                {
                    if (Word.ToUpper()[i] == c)
                    {
                        sb.Append(Word[i]);
                        foundChar = true;
                    }
                    else
                    {
                        sb.Append("_");
                    }
                }
                else
                {
                    sb.Append(GuessedWord[i]);
                }
            }
            sb.Append(GuessedWord[GuessedWord.Length - 1]);
            GuessedWord = sb.ToString();

            if (!foundChar)
            {
                WrongChars += " " + c;
                WrongCharsCount++;
            }

            sb.Clear(); sb.Append(GuessedWord[0]);

            for (int i = 1; i < GuessedWord.Length; i++)
            {
                sb.Append(" " + GuessedWord[i]);
            }

            DisplayWord = sb.ToString();
            Status = GuessedWord.IndexOf("_") == -1 ? "Success" : (WrongCharsCount > 5 ? "Failed" : "");
            if(Status != "")
            {
                repository.RecordGame(playerID, wordID, WrongCharsCount, 0, Status == "Success");
            }

        }

        public static GamesResult GetGamesResult(string username)
        {
            IHangmanRepository repository = HangmanRepositoryFactory.CreateRepository();
            int playerID = repository.GetPlayerID(username);
            GamesResult result = repository.GetGamesResultForPlayer(playerID);
            return result;
        }
    }
}