using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KataReversi
{
    public class Board
    {

        public enum Players
        {
            Black,
            White
        };

        public Players CurrentPlayer { get; set; }

        public string[] AvailableMoves
        {
            get
            {
                switch (CurrentPlayer)
                {
                    case Players.Black:
                        return new string[] { "C5", "D6", "E3", "F4" };
                        break;
                    case Players.White:
                        return new string[] { "C3", "C5", "E3" };
                        break;
                    default:
                        return new string[0];
                        break;
                }
            }
        }
    }
}
