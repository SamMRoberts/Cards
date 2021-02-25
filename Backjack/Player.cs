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
        }
    }
}
