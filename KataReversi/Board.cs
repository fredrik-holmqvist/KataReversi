using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KataReversi
{
    public class Board
    {
        private const int A = 65;


        private class Position
        {
            public int X { get; set; }
            public int Y { get; set; }
        }

        private IEnumerable<Position> blackPositions;
        private IEnumerable<Position> whitePositions;


        public Board(string black, string white)
        {
            blackPositions = (from cord in black.Split(',')
                              select new Position { X = Lookup(cord[0]), Y = Convert.ToInt32(cord[1].ToString()) - 1 }).ToList();

            whitePositions = (from cord in white.Split(',')
                              select new Position { X = Lookup(cord[0]), Y = Convert.ToInt32(cord[1].ToString()) - 1 }).ToList();

        }

        private int Lookup(char letter)
        {
            return Convert.ToInt32(letter) - A;
        }

        private char ReverseLookup(int number)
        {
            return Convert.ToChar(number + A);
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
                        moves.Add(string.Format("{0}{1}", ReverseLookup(move.X), move.Y + 1));
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
