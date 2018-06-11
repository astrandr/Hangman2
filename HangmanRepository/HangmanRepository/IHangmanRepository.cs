using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangmanRepository
{
    public interface IHangmanRepository
    {
        #region Players

        //TODO Add comments to all methods
        bool    AddPlayer(string username, string password); //fails if user exist
        int     GetPlayerID(string username); //returns 0 if username is invalid
        bool    VerifyUser(int id, string username, string password); //returns false if incorrect username + password is provided
        bool    UpdatePlayerByID(int id, string username, string password); // returns false if failed
        void    RemovePlayerByID(int id); // exits silently if id is not correct

        #endregion

        #region Categories

        int[] GetCategories();
        string GetCategoryByID(int categoryID);

        #endregion

        #region Words

        int[] GetWords(int categoryID);
        string[] GetWordByID(int id);

        #endregion

        #region Games

        void RecordGame(int playerID, int wordID, int wrongChars, int correctChars, bool success);
        GamesResult GetGamesResultForPlayer(int playerID);

        #endregion

    }
}
