using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Blackjack.Models.Card;

namespace Blackjack_Unit_Tests
{
    [TestClass]
    public class CardTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            string suit = "Heart";
            string face = "Ace";
            Card newCard = new Card
            {
                Suit = suit,
                Face = face
            };
        }
    }
}
