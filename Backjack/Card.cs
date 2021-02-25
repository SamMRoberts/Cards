using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace BlackJack
{
    public class Card
    {
        public CardSuit Suit { get; set; }
        private CardFace face;
        //public CardFace Face { get; set; }
        public CardFace Face
        {
            get { return this.face; }
            set
            {
                this.face = value;
                this.Value = SetValue(this.Face);
            }
        }
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
            //int value = (int)Enum.Parse(typeof(Face), card.Face.ToString());
            int v = valueMap[$"{f}"];
            Console.WriteLine($"Value is: {v}");
            return v;
        }
    }

    public enum CardFace
    {
        Ace,
        Two,
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
        King
    }

    public enum CardSuit
    {
        Heart,
        Diamond,
        Club,
        Spade
    }

}
