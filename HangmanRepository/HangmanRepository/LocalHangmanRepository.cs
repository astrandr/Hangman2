using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;

namespace HangmanRepository
{
    public class LocalHangmanRepository : IHangmanRepository
    {

        private string connectionString = ConfigurationManager.AppSettings["connectionString"];

        public bool AddPlayer(string username, string password)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString;
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = @"insert into Hangman..Players (UserName, UserPassword) values (@username, @password);";
                command.Connection = connection;
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password", password);
                command.ExecuteScalar();
                return true;
            }
            catch
            {
                //Add logging here
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        public int GetPlayerID(string username)
        {
            int result = 0;

            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString;
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = @"select PlayerID from Hangman..Players players where players.UserName = @username ";
                command.Parameters.AddWithValue("@username", username);
                command.Connection = connection;
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    result = reader.GetInt32(0);
                }
            }
            catch
            {
                //Add logging here
                throw;
            }

            connection.Close();

            return result;
        }

        public bool VerifyUser(int id, string username, string password)
        {
            bool result = false;

            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString;
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = @"select PlayerID from Hangman..Players players where players.PlayerID = @id and players.UserName = @username and players.UserPassword = @password ";
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password", password);
                command.Connection = connection;
                SqlDataReader reader = command.ExecuteReader();
                result = reader.HasRows;
            }
            catch
            {
                //Add logging here
            }
            finally
            {
                connection.Close();
            }

            return result;
        }

        public bool UpdatePlayerByID(int id, string username, string password)
        {
            bool result = false;

            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString;
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = @"update Hangman..Players set UserName = @username, UserPassword=@password where PlayerID = @id";
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password", password);
                command.Connection = connection;
                command.ExecuteScalar();
                result = true;
            }
            catch
            {
                //Add logging here
                throw;
            }
            finally
            {
                connection.Close();
            }

            return result;
        }

        public void RemovePlayerByID(int id)
        {

            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString;
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = @"delete from Hangman..Players where PlayerID = @id ";
                command.Connection = connection;
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteScalar();
            }
            catch
            {
                //Add logging here
                throw; //TODO Set specific exception
            }
            finally
            {
                connection.Close();
            }
        }

        public int[] GetCategories()
        {
            int[] resultList = null;

            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString;
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = @"select CategoryID from Hangman..Categories";
                command.Connection = connection;
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    List<int> rows = new List<int>();
                    while (reader.Read())
                    {
                        rows.Add(reader.GetInt32(0));
                    }

                    resultList = rows.ToArray();
                }
            }
            catch
            {
                //Add logging here
                throw;
            }
            finally
            {
                connection.Close();
            }

            return resultList;

        }

        public int[] GetWords(int categoryID)
        {
            int[] resultList = null;

            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString;
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = @"select WordID from Hangman..Words words where words.CategoryID = @categoryID";
                command.Parameters.AddWithValue("@categoryID", categoryID);
                command.Connection = connection;
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    List<int> rows = new List<int>();
                    while (reader.Read())
                    {
                        rows.Add(reader.GetInt32(0));
                    }

                    resultList = rows.ToArray();

                }
            }
            catch
            {
                //Add logging here
                throw;
            }
            finally
            {
                connection.Close();
            }

            return resultList;
        }

        public string[] GetWordByID(int id)
        {
            string[] result = null;

            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString;
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = @"select WordText, WordDescription from Hangman..Words words where words.WordID = @ID ";
                command.Parameters.AddWithValue("@ID", id);
                command.Connection = connection;
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    result = new string[]{reader.GetString(0), reader.GetString(1)};
                }
            }
            catch
            {
                //Add logging here
                throw;
            }
            finally
            {
                connection.Close();
            }

            return result;
        }

        public string GetCategoryByID(int id)
        {
            string result = null;

            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString;
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = @"select CategoryName from Hangman..Categories where CategoryID = @ID ";
                command.Parameters.AddWithValue("@ID", id);
                command.Connection = connection;
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    result = reader.GetString(0);
                }
            }
            catch
            {
                //Add logging here
                throw;
            }
            finally
            {
                connection.Close();
            }

            return result;
        }

        public void RecordGame(int playerID, int wordID, int wrongChars, int correctChars, bool success)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString;
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = @"insert into Hangman..Games (PlayerId, WordID, CorrectChars, WrongChars, Success) values (@playerID, @wordID, @wrongChars, @correctChars, @success)";
                command.Parameters.AddWithValue("@playerID", playerID);
                command.Parameters.AddWithValue("@wordID", wordID);
                command.Parameters.AddWithValue("@wrongChars", wrongChars);
                command.Parameters.AddWithValue("@correctChars", correctChars);
                command.Parameters.AddWithValue("@success", success);

                command.ExecuteScalar();
            }
            catch
            {
                //Add logging here
                throw;
            }
            finally
            {
                connection.Close();
            }
        }

        public GamesResult GetGamesResultForPlayer(int playerID)
        {
            GamesResult result = new GamesResult();

            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString;
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = @"select COUNT(*) as NUM_GAMES, SUM(wrongChars) as WRONG_CHARS, SUM(correctChars) as CORRECT_CHARS from Games where PlayerID = @playerID GROUP BY playerID;";
                command.Parameters.AddWithValue("@playerID", playerID);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    result.NumberOfGames = reader.GetInt32(0);
                    result.NumberOfCharGuesses = reader.GetInt32(1) + reader.GetInt32(2);
                }
                reader.Close();

                command.CommandText = @"select COUNT(*) as NUM_SUCCESSES from Games where PlayerID = @playerID and Success=1 GROUP BY playerID;";
                reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    result.NumberOfSuccess = reader.GetInt32(0);
                }

                result.NumberOfFails = result.NumberOfGames - result.NumberOfSuccess;
            }
            catch
            {
                //Add logging here
                throw;
            }
            finally
            {
                connection.Close();
            }

            return result;
        }
    }
}
