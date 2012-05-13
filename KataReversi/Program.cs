using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KataReversi
{
    class Program
    {
        static void Main(string[] args)
        {
            var board = new Board("E4,D5", "D4,E5");
            board.CurrentPlayer = Board.Players.Black;
            board.AvailableMoves.ToString();
        }
    }
}
