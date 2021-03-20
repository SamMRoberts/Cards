using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Blackjack.Engine.Models
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
                for (int fc = 2; fc < numOfFaces + 2; fc++)
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
            Card card = Cards[0];
            Cards.RemoveAt(0);
            return card;
        }

        // Shuffle cards
        public async void Shuffle()
        {

            Task<List<Card>> shuffleTask = AsyncShuffle();
            this.Cards = await shuffleTask;
        }


        public void SyncShuffle()
        {
            List<Card> newCards = new List<Card>();
            List<int> shuffled = new List<int>();
            int totalCards = Cards.Count;
            Random random = new Random();
            do
            {
                bool alreadyShuffled = false;
                while (alreadyShuffled == false)
                {
                    int getIndex = random.Next(totalCards);
                    Card getCard = this.Cards[getIndex];

                    if (shuffled.Contains(getIndex))
                    {
                        alreadyShuffled = true;
                    }
                    else
                    {
                        newCards.Add(getCard);
                        shuffled.Add(getIndex);
                    }
                }
                //Thread.Sleep(20);
            } while (newCards.Count != totalCards);
            Cards = newCards;
        }

        private async Task<List<Card>> AsyncShuffle()
        {
            List<Card> newCards = new List<Card>();
            List<int> shuffled = new List<int>();
            int totalCards = Cards.Count;
            Random random = new Random();
            //Console.WriteLine("Shuffling cards.");
            await Task.Run(() =>
            {
                do
                {
                    bool alreadyShuffled = false;
                    while (alreadyShuffled == false)
                    {
                        int getIndex = random.Next(totalCards);
                        Card getCard = this.Cards[getIndex];

                        if (shuffled.Contains(getIndex))
                        {
                            alreadyShuffled = true;
                        }
                        else
                        {
                            newCards.Add(getCard);
                            shuffled.Add(getIndex);
                        }
                    }
                    Thread.Sleep(20);
                } while (newCards.Count != totalCards);
                //Console.WriteLine("Shuffling complete.");
            });
            //Cards = newCards;
            return newCards;
        }

    }
}
