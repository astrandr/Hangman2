using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangmanRepository
{
    public class HangmanRepositoryFactory
    {
        public static IHangmanRepository CreateRepository()
        {
            return new LocalHangmanRepository();
        }
    }
}
