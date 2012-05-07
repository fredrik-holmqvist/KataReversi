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
            Black
        };

        public Players CurrentPlayer { get; set; }

        public string[] AvailableMoves
        {
            get
            {
                return new string[] { "C5", "D6", "E3", "F4" };
            }
        }
    }
}
