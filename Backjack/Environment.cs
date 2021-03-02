using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace BlackJack
{
    class Environment
    {
        private List<Card> Cards = new List<Card>();    // Hold card while shuffling
        public Deck Deck = new Deck(false);    // Deck in play after shuffle
        private List<Deck> NewDecks = new List<Deck>();    // Card decks not in play       
        private List<Card> BonePile = new List<Card>();    // Cards not in play
        private readonly int NumberOfDecks = 2;    // Total number of decks to have in the game environment
        private readonly string[] acceptedValues = { "hit", "stand", "quit", "newgame" };    // Array of acceptable input values
        private List<Player> Seats = new List<Player>();
        private Player Dealer;
        private Player Player;

        // Environment is like a virtual game table.  Card decks and players are initialized here.
        public Environment()
        {
            if (EnvironmentHasDecks() == false)
            {
                this.NewDecks.Clear();
                GetDecks();
                PutDecksInPlay();
                Shuffle();
                Dealer = new Player("Dealer", true);
                Seats.Add(Dealer);
                this.Speak("Enter your name.");
                string name = this.Listen(false);
                Player = new Player(name);
                Seats.Add(Player);
                this.Speak($"Welcome to the game {Player.Name}");
                Deal();
                string input;

                do
                {
                    input = this.Listen();
                    CheckInput(input);
                } while (input != "quit");
            }
        }

        // Check to see if the required number of decks are in the environment
        public bool EnvironmentHasDecks()
        {
            int currentNumberOfDecks = this.NewDecks.Count;
            if (currentNumberOfDecks != NumberOfDecks)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        // Gets a new deck of cards
        public void GetDecks()
        {
            for (int x = 1; x <= NumberOfDecks; x++)
            {
                Deck deck = new Deck();
                this.NewDecks.Add(deck);
            }
        }

        // Move decks to cards variable to put them in play
        public void PutDecksInPlay()
        {
            foreach (Deck deck in this.NewDecks)
            {
                foreach (Card card in deck.Cards)
                {
                    this.Cards.Add(card);
                }
            }
            this.NewDecks.Clear();
        }

        // Shuffle cards
        public void Shuffle()
        {
            List<Card> newCards = new List<Card>();
            List<int> shuffled = new List<int>();
            int totalCards = this.Cards.Count;
            Random random = new Random();

            Console.WriteLine("Shuffling cards.");
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
            } while (newCards.Count != totalCards);
            //this.Cards = newCards;
            Deck.Cards = newCards;
        }

        public string Listen()
        {
            return Listen(true);
        }

        public string Listen(bool validate)
        {
            bool isAcceptedInput = !validate;
            string input;

            do
            {
                Console.Write("> ");
                input = Console.ReadLine();

                if (validate)
                {
                    string result = acceptedValues.FirstOrDefault(x => x == input);
                    if (result != null)
                    {
                        isAcceptedInput = true;
                    }
                    else
                    {
                        Console.WriteLine("Please enter an acceptable value.");
                    }
                }
            } while (isAcceptedInput == false);

            return input;
        }

        public void Speak(string output)
        {
            Console.WriteLine($"[{GetTime()}] : {output}");
        }

        public string GetTime()
        {
            DateTime now = DateTime.Now;
            string time = now.ToString("HH:mm:ss");
            return time;
        }

        private void Deal()
        {
            for (int i = 0; i < 2; i++)
            {
                foreach (Player player in this.Seats)
                {
                    Hit(player);
                }
            }
            foreach (Player player in this.Seats)
            {
                AnnounceHand(player);
            }
        }

        private void AnnounceHand(Player player)
        {
            this.Speak($"{player.Name}'s hand: {player.EnumerateHand()}");
        }

        private void CheckInput(string input)
        {
            switch (input.ToLower())
            {
                case "hit":
                    Hit(Player);
                    AnnounceHand(Player);
                    break;
            }
        }

        private void Hit(Player player)
        {
            Card card = this.Deck.GetCard();
            player.GiveCard(card);
        }
    }
}
