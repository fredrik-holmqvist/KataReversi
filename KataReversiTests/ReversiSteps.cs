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

        [Given(@"I have an starting board")]
        public void GivenIHaveAnStartingBoard()
        {
            board = new Board();
        }

        [When(@"I enter that it's (.*) turn")]
        public void WhenIEnterThatItIsPlayersTurn(string player)
        {
            board.CurrentPlayers = Board.Players.Black;
        }

        [Then(@"the result should be '(.*)'")]
        public void ThenTheResultShouldBeValue(string moves)
        {
            Assert.AreEqual(moves.Split(','), board.AvailableMoves);
        }

    }
}
