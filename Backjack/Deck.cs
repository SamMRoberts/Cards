using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Text;

namespace BlackJack
{
    public class Deck
    {
        public List<Card> Cards = new List<Card>();

        public Deck()
        {
            NewDeck();
        }

        public Deck(bool createNew)
        {
            if (createNew)
            {
                NewDeck();
            }
            else
            {
                EmptyDeck();
            }
        }

        private void EmptyDeck()
        {

        }

        public void NewDeck()
        {
            int numOfSuits = CardSuit.GetNames(typeof(CardSuit)).Length;
            int numOfFaces = CardFace.GetNames(typeof(CardFace)).Length;

            for (int sc = 0; sc < numOfSuits; sc++)
            {
                for (int fc = 0; fc < numOfFaces; fc++)
                {
                    Card newCard = new Card
                    {
                        Suit = (CardSuit)sc,
                        Face = (CardFace)fc
                    };
                    Cards.Add(newCard);
                }
            }
        }

        public Card GetCard()
        {
            Card card = this.Cards[0];
            this.Cards.RemoveAt(0);
            return card;
        }
    }
}
