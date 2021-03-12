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
        private readonly string[] acceptedValues = { "hit", "stand", "quit" };    // Array of acceptable input values
        private List<Player> Seats = new List<Player>();
        private readonly Player Dealer;
        private readonly Player Player;

        // Environment is like a virtual game table.  Card decks and players are initialized here.
        public Environment()
        {
            if (EnvironmentHasDecks() == false)
            {
                NewDecks.Clear();
                GetDecks();
                PutDecksInPlay();
                Deck.Shuffle();
                Dealer = new Player("Dealer", true);
                Seats.Add(Dealer);
                Speak("Enter your name.");
                string name = Listen(false);
                Player = new Player(name);
                Seats.Add(Player);
                Speak($"Welcome to the game {Player.Name}");
                Deal();
                string input;

                // Do while input is not "quit"
                do
                {
                    input = Listen();
                    CheckInput(input);
                } while (input != "quit");
            }
        }

        // Check to see if the required number of decks are in the environment
        public bool EnvironmentHasDecks()
        {
            int currentNumberOfDecks = NewDecks.Count;
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
        private void GetDecks()
        {
            for (int x = 1; x <= NumberOfDecks; x++)
            {
                Deck deck = new Deck();
                NewDecks.Add(deck);
            }
        }

        // Move decks to cards variable to put them in play
        private void PutDecksInPlay()
        {
            foreach (Deck deck in NewDecks)
            {
                foreach (Card card in deck.Cards)
                {
                    Deck.Cards.Add(card);
                }
            }
            NewDecks.Clear();
        }

        // Listens for input
        public string Listen()
        {
            return Listen(true);
        }
        // Listens for input and validates input
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
                        Console.WriteLine("Please enter an acceptable value (\"hit\", \"stand\" or \"quit\").");
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
            Dealer.ClearHand();
            Player.ClearHand();
            Console.Clear();
            for (int i = 0; i < 2; i++)
            {
                foreach (Player player in Seats)
                {
                    Hit(player);
                }
            }
            foreach (Player player in Seats)
            {
                AnnounceHand(player);
            }
        }

        private void AnnounceHand(Player player)
        {
            Speak($"{player.Name}'s hand: {player.EnumerateHand()}");
        }

        // Perform functions based on input
        private void CheckInput(string input)
        {
            if ((Dealer.Stand == true) & (Player.Stand == true))
            {
                CheckWinner();           
            }
            else
            {
                switch (input.ToLower())
                {
                    case "hit":
                        Hit(Player, true, true);
                        if (Player.Busted)
                        {
                            Pause();
                            Deal();
                        }
                        break;
                    /*case "deal":
                        Deal();
                        break;*/
                    case "stand":
                        Player.Stand = true;
                        DealerTurn();
                        break;
                }
            }
        }

        private void CheckWinner()
        {
            if (Player.HandValue > Dealer.HandValue)
            {
                Player.Wins++;
                Dealer.Losses++;
                Console.WriteLine($"{Player.Name} WINS!");
                Pause();
                Deal();
            }
            else if (Dealer.HandValue > Player.HandValue)
            {
                Dealer.Wins++;
                Player.Losses++;
                Console.WriteLine($"{Dealer.Name} WINS!");
                Pause();
                Deal();
            }
            // House wins by default
            else
            {
                Dealer.Wins++;
                Player.Losses++;
                Console.WriteLine($"{Dealer.Name} WINS!");
                Pause();
                Deal();
            }            
        }

        // Player hit without announce and bust check
        private void Hit(Player player)
        {
            Card card = Deck.GetCard();
            player.GiveCard(card);            
        }

        // Player hit with announce and bust check
        private void Hit(Player player, bool announce, bool checkBust)
        {
            Card card = Deck.GetCard();
            player.GiveCard(card);
            if (announce)
            {
                AnnounceHand(player);
            }
            if (checkBust)
            {
                if (CheckBust(player))
                {
                    player.Busted = true;
                }
            }            
        }

        // Check if player busted
        private bool CheckBust(Player player)
        {
            if (player.HandValue > 21)
            {
                player.Busted = true;
                Console.WriteLine($"{player.Name} BUSTED!");
                player.Losses++;
                if (player.GetIsDealer())
                {
                    Player.Wins++;
                }
                else
                {
                    Dealer.Wins++;
                }
                return true;
            }
            return false;
        }

        // Dealer turn: Hits if < 17, stands if >= 17
        private void DealerTurn()
        {
            while (Dealer.HandValue < 17)
            {
                Hit(Dealer, true, true);
            }
            if ((Dealer.HandValue >= 17) & (Dealer.Busted == false))
            {
                CheckWinner();
                Dealer.Stand = true;                
            }
            if (Dealer.Busted)
            {
                Pause();
                Deal();
            }
        }

        private static void Pause()
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true);
        }
    }
}
