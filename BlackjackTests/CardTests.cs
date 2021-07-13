using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Blackjack.Engine.Models;

namespace Blackjack_Unit_Tests
{
    [TestClass]
    public class CardTests
    {
        [TestMethod]
        public void ValueTest()
        {
            int expected = 11;
            Card newCard = new Card
            {
                Suit = CardSuit.Heart,
                Face = CardFace.Ace
            };
            int actual = newCard.Value;
            Assert.AreEqual(expected, actual, "Card value is incorrect.");
        }
    }
}
