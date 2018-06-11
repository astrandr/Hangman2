using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangmanRepository
{
    public struct GamesResult
    {
        public int NumberOfGames { get; set; }
        public int NumberOfSuccess { get; set; }
        public int NumberOfFails { get; set; }
        public int NumberOfCharGuesses { get; set; }
        public int WholeWordSuccess { get; set; }
    }
}
