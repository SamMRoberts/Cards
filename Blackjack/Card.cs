using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace Blackjack
{
    public class Card
    {
        public CardSuit Suit { get; set; }
        private CardFace face;
        public CardFace Face
        {
            get { return this.face; }
            set
            {
                this.face = value;
                this.Value = SetValue(this.Face);
                int f = (int)value;
                if ((f >= 2) & (f <= 10))
                {
                    this.DisplayFace = f.ToString();    // Sets display face to Int value or first letter of face cards.  (Ex: 4 for Four, K for King)
                }
                else
                {
                    this.DisplayFace = this.face.ToString().Substring(0,1);
                }      
            }
        }
        public string DisplayFace { get; set; }
        public int Value { get; set; }

        public Card()
        {

        }

        public int SetValue(CardFace f)
        {
            Dictionary<string, int> valueMap = new Dictionary<string, int>();
            valueMap.Add("Ace", 11);
            valueMap.Add("Two", 2);
            valueMap.Add("Three", 3);
            valueMap.Add("Four", 4);
            valueMap.Add("Five", 5);
            valueMap.Add("Six", 6);
            valueMap.Add("Seven", 7);
            valueMap.Add("Eight", 8);
            valueMap.Add("Nine", 9);
            valueMap.Add("Ten", 10);
            valueMap.Add("Jack", 10);
            valueMap.Add("Queen", 10);
            valueMap.Add("King", 10);
            int v = valueMap[$"{f}"];
            return v;
        }
    }

    public enum CardFace
    {
        Two = 2,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King,
        Ace
    }

    public enum CardSuit
    {
        Heart,
        Diamond,
        Club,
        Spade
    }

}
