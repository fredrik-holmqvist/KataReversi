using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace KataReversiTests
{
    [Binding]
    class ReversiSteps
    {
        [Given(@"I have an starting board")]
        public void GivenIHaveAnStartingBoard()
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"I enter that it's (.*) turn")]
        public void WhenIEnterThatItIsPlayersTurn(string player)
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"the result should be (.*)")]
        public void ThenTheResultShouldBeValue(string moves)
        {
            ScenarioContext.Current.Pending();
        }

    }
}
