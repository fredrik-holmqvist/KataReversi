using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KataReversi
{
    public class Board
    {
        private const int A = 65;

        private class Cordinate
        {
            public int X { get; set; }
            public int Y { get; set; }
        }

        private IEnumerable<Cordinate> blackPositions;
        private IEnumerable<Cordinate> whitePositions;

        public Board(string black, string white)
        {
            blackPositions = (from cord in black.Split(',')
                              select new Cordinate { X = Lookup(cord[0]), Y = Convert.ToInt32(cord[1].ToString()) - 1 }).ToList();

            whitePositions = (from cord in white.Split(',')
                              select new Cordinate { X = Lookup(cord[0]), Y = Convert.ToInt32(cord[1].ToString()) - 1 }).ToList();
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
                return from currentPosition in GetCurrentPlayer()
                       from move in FindMoves(currentPosition, GetCurrentPlayer(), GetOpposingPlayer())
                       select string.Format("{0}{1}", ReverseLookup(move.X), move.Y + 1);
            }
        }

        private int Lookup(char letter)
        {
            return Convert.ToInt32(letter) - A;
        }

        private char ReverseLookup(int number)
        {
            return Convert.ToChar(number + A);
        }

        private IEnumerable<Cordinate> GetOpposingPlayer()
        {
            switch (CurrentPlayer)
            {
                case Players.Black:
                    return whitePositions;
                case Players.White:
                    return blackPositions;
                default:
                    return whitePositions;
            }
        }

        private IEnumerable<Cordinate> GetCurrentPlayer()
        {
            switch (CurrentPlayer)
            {
                case Players.Black:
                    return blackPositions;
                case Players.White:
                    return whitePositions;
                default:
                    return blackPositions;
            }
        }

        private IEnumerable<Cordinate> FindMoves(Cordinate currentPosition, IEnumerable<Cordinate> currentPlayer, IEnumerable<Cordinate> opposingPlayer)
        {
            return from direction in FindPossibleDirections(currentPosition, opposingPlayer)
                   let pos = SearchDirection(currentPosition, direction, currentPlayer, opposingPlayer)
                   where pos != null
                   select pos;
        }

        private Cordinate SearchDirection(Cordinate currentPosition, Cordinate direction, IEnumerable<Cordinate> currentPlayer, IEnumerable<Cordinate> opposingPlayer)
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
                        return new Cordinate() { X = currentX, Y = currentY };
                    }
                }
            }

            return null;
        }

        private IEnumerable<Cordinate> FindPossibleDirections(Cordinate pos, IEnumerable<Cordinate> opposingPlayer)
        {
            for (int x = pos.X - 1; x <= pos.X + 1; x++)
            {
                for (int y = pos.Y - 1; y <= pos.Y + 1; y++)
                {
                    if (opposingPlayer.Any(p => p.X == x && p.Y == y))
                    {
                        yield return new Cordinate() { X = x - pos.X, Y = y - pos.Y };
                    }
                }
            }
        }
    }
}
