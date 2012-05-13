using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using KataReversi;
using NUnit.Framework;

namespace KataReversiTests
{
    [Binding]
    class ReversiSteps
    {
        Board board;

        [Given(@"I have this board:")]
        public void GivenIHaveThisBoard(Table table)
        {

            board = new Board(table.Rows[0][0], table.Rows[0][1]);
        }

        [When(@"I enter that it's (.*) turn")]
        public void WhenIEnterThatItIsPlayersTurn(string player)
        {
            switch (player)
            {
                case "blacks":
                    board.CurrentPlayer = Board.Players.Black;
                    break;
                default:
                    board.CurrentPlayer = Board.Players.White;
                    break;
            }
        }

        [Then(@"the result should be '(.*)'")]
        public void ThenTheResultShouldBeValue(string moves)
        {
            var result = board.AvailableMoves.ToList();
            foreach (var move in moves.Split(','))
            {
                Assert.Contains(move, result);
            }
        }

    }
}
