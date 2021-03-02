using System;
using System.Collections.Generic;
using System.Text;

namespace BlackJack
{
    public class Player
    {
        public string Name;
        public List<Card> Hand = new List<Card>();
        public int HandValue;
        private bool IsDealer = false;
        private static int instances = 0;

        public Player()
        {
            instances++;
        }

        ~Player()
        {
            instances--;
        }

        public Player(string name)
        {
            instances++;
            this.Name = name;
        }

        public Player(string name, bool isDealer)
        {
            instances++;
            this.Name = name;
            this.IsDealer = isDealer;
        }

        public static int GetInstances()
        {
            return instances;
        }

        public bool GetIsDealer()
        {
            return this.IsDealer;
        }

        public void GiveCard(Card card)
        {
            this.Hand.Add(card);
            this.HandValue = this.GetHandValue();
            this.EvaluateAceValue();
        }

        private int GetHandValue()
        {
            int value = new int();
            foreach (Card card in this.Hand)
            {
                value += card.Value;
            }
            return value;
        }

        private bool HandHasAce()
        {
            foreach (Card card in this.Hand)
            {
                if (card.Face.ToString().ToLower() == "ace")
                {
                    return true;
                }
            }
            return false;
        }

        private void EvaluateAceValue()
        {
            if ((this.HandValue > 21) & (this.HandHasAce()))
            {
                foreach (Card c in this.Hand)
                {
                    if ((c.Face.ToString().ToLower() == "ace") & (c.Value == 11))
                    {
                        this.SetAceLowerValue(c);
                        this.HandValue = this.GetHandValue();
                        break;
                    }
                }
            }
        }

        // Set Ace value to 1 instead of 11
        private Card SetAceLowerValue(Card card)
        {
            if (card.Face != CardFace.Ace)
            {
                throw new Exception("Cannot lower the value of a card that is not an Ace");
            }
            else
            {
                card.Value = 1;
            }
            return card;
        }

        public string EnumerateHand()
        {
            string hand = null;
            foreach (Card card in this.Hand)
            {
                hand += $"[{card.Face}:{card.Suit}] ";
            }
            hand += $"Value: {this.HandValue}";
            return hand;
        }
    }
}
