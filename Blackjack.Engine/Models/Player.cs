using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Blackjack.Engine.Models
{
    public class Player : INotifyPropertyChanged
    {
        private string _displayHand;
        public string Name { get; set; }
        public List<Card> Hand = new List<Card>();
        public int HandValue;
        private bool IsDealer = false;
        public bool Busted = false;
        public bool Stand = false;
        public int Wins = 0;
        public int Losses = 0;
        public string DisplayHand
        {
            get { return _displayHand; }
            set
            {
                _displayHand = value;
                OnPropertyChanged(nameof(DisplayHand));
            }
        }

        public Player()
        {
        }

        public Player(string name)
        {
            this.Name = name;
        }

        public Player(string name, bool isDealer)
        {
            this.Name = name;
            IsDealer = isDealer;
        }

        public bool GetIsDealer()
        {
            return IsDealer;
        }

        public void GiveCard(Card card)
        {
            Hand.Add(card);
            HandValue = GetHandValue();
            EvaluateValue();
            this.DisplayHand = EnumerateHand();
        }

        private int GetHandValue()
        {
            int value = new int();
            foreach (Card card in Hand)
            {
                value += card.Value;
            }
            return value;
        }

        // Check if hand has an Ace. Used for determining Ace value of 1 or 11
        private bool HandHasAce()
        {
            foreach (Card card in Hand)
            {
                if (card.Face.ToString().ToLower() == "ace")
                {
                    return true;
                }
            }
            return false;
        }


        // Evaluate if Ace should be counted as 1 or 11
        private void EvaluateValue()
        {
            if ((HandValue > 21) & (HandHasAce()))
            {
                foreach (Card c in Hand)
                {
                    if ((c.Face.ToString().ToLower() == "ace") & (c.Value == 11))
                    {
                        SetAceLowerValue(c);
                        HandValue = GetHandValue();
                        break;
                    }
                }
            }
            else if (HandValue > 21)
            {
                //this.Busted = true;
                // Busted
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

        // Returns a string containing cards in hand, hand value and wins:losses
        public string EnumerateHand()
        {
            string hand = null;
            foreach (Card card in Hand)
            {
                hand += $"[{card.DisplayFace}:{card.Suit}] ";
            }
            hand += $"(Value: {HandValue}) <W: {Wins} L: {Losses}>";
            return hand;
        }

        public void ClearHand()
        {
            Busted = false;
            Stand = false;
            Hand.Clear();
            HandValue = 0;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
