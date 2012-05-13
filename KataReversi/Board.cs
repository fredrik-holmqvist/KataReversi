using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KataReversi
{
    public class Board
    {

        private class Position
        {
            public int X { get; set; }
            public int Y { get; set; }
        }

        private int[,] _board;

        private IEnumerable<Position> blackPositions;
        private IEnumerable<Position> whitePositions;

        Dictionary<char, int> lookup = new Dictionary<char, int>{ 
            {'A',0},
            {'B',1},
            {'C',2},
            {'D',3},
            {'E',4},
            {'F',5},
            {'G',6},
            {'H',7}};

        Dictionary<int, char> reverseLookup = new Dictionary<int, char>{ 
            {0,'A'},
            {1,'B'},
            {2,'C'},
            {3,'D'},
            {4,'E'},
            {5,'F'},
            {6,'G'},
            {7,'H'}};

        public Board(string black, string white)
        {
            blackPositions = from piece in black.Split(',')
                             select new Position { X = lookup[piece[0]], Y = Convert.ToInt32(piece[1].ToString()) - 1 };

            whitePositions = from piece in white.Split(',')
                             select new Position { X = lookup[piece[0]], Y = Convert.ToInt32(piece[1].ToString()) - 1 };

            //_board = new int[8, 8];

            //for (int i = 0; i < 8; i++)

            //    for (int j = 0; j < 8; j++)
            //    {
            //        if (blackPositions.Any(p => p.X == i && p.Y == j))
            //        {
            //            _board[i, j] = 1;
            //        }
            //        else if (whitePositions.Any(p => p.X == i && p.Y == j))
            //        {
            //            _board[i, j] = 2;
            //        }
            //        else
            //        {
            //            _board[i, j] = 0;
            //        }
            //    }
        }



        public enum Players
        {
            Black,
            White
        };

        public Players CurrentPlayer { get; set; }

        public IEnumerable<string> AvailableMoves
        {
            get
            {
                var moves = new List<string>();
                IEnumerable<Position> currentPlayer = null;
                IEnumerable<Position> opposingPlayer = null;

                switch (CurrentPlayer)
                {
                    case Players.Black:
                        currentPlayer = blackPositions;
                        opposingPlayer = whitePositions;
                        break;
                    case Players.White:
                        currentPlayer = whitePositions;
                        opposingPlayer = blackPositions;
                        break;
                }




                foreach (var currentPosition in currentPlayer)
                {
                    foreach (var move in FindMoves(currentPosition, currentPlayer, opposingPlayer))
                    {
                        moves.Add(string.Format("{0}{1}", reverseLookup[move.X], move.Y + 1));
                    }

                }

                return moves;
            }

        }

        private IEnumerable<Position> FindMoves(Position currentPosition, IEnumerable<Position> currentPlayer, IEnumerable<Position> opposingPlayer)
        {
            List<Position> result = new List<Position>();
            var possibleDirections = FindPossibleDirections(currentPosition, opposingPlayer);

            foreach (var direction in possibleDirections)
            {
                var pos = SearchDirection(currentPosition, direction, currentPlayer, opposingPlayer);
                if (pos != null)
                {
                    result.Add(pos);
                }
            }

            return result;
        }

        private Position SearchDirection(Position currentPosition, Position direction, IEnumerable<Position> currentPlayer, IEnumerable<Position> opposingPlayer)
        {
            var currentX = currentPosition.X;
            var currentY = currentPosition.Y;

            while (currentX >= 0 && currentX <= 7 && currentY >= 0 && currentY <= 7)
            {
                currentX += direction.X;
                currentY += direction.Y;

                if (!opposingPlayer.Any(p => p.X == currentX && p.Y == currentY))
                {
                    if (!currentPlayer.Any(p => p.X == currentX && p.Y == currentY))
                    {
                        return new Position() { X = currentX, Y = currentY };
                    }
                }
            }

            return null;
        }

        private IEnumerable<Position> FindPossibleDirections(Position pos, IEnumerable<Position> opposingPlayer)
        {
            for (int i = pos.X - 1; i <= pos.X + 1; i++)
            {
                for (int j = pos.Y - 1; j <= pos.Y + 1; j++)
                {
                    if (i >= 0 && i <= 7 && j >= 0 && j <= 7)
                    {
                        if (opposingPlayer.Any(p => p.X == i && p.Y == j))
                        {
                            yield return new Position() { X = i - pos.X, Y = j - pos.Y };
                        }
                    }
                }
            }
        }
    }
}
