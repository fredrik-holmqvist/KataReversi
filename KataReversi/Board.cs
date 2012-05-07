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

        public Players CurrentPlayers { get; set; }

        public string[] AvailableMoves { get; set; }
    }
}
