using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Blackjack.Engine.Models
{
    public class Environment : INotifyPropertyChanged
    {
        private Player _dealer;
        private Player _player;
        private string _output;
        private bool _enableDeal;
        private bool _enableTurn;

        public Player Dealer
        {
            get { return _dealer; }
            set
            {
                _dealer = value;
                OnPropertyChanged(nameof(Dealer));
            }
        }

        public Player Player
        {
            get { return _player; }
            set
            {
                _player = value;
                OnPropertyChanged(nameof(Player));
            }
        }

        public string Output
        {
            get { return _output; }
            set
            {
                _output += $"{value}\r\n";
                OnPropertyChanged(nameof(Output));
            }
        }

        
        public bool EnableDeal
        {
            get { return _enableDeal; }
            set
            {
                _enableDeal = value;
                OnPropertyChanged(nameof(EnableDeal));
            }
        }
        public bool EnableTurn
        {
            get { return _enableTurn; }
            set
            {
                _enableTurn = value;
                OnPropertyChanged(nameof(EnableTurn));
            }
        }
        public List<Card> Cards = new List<Card>();    // Hold card while shuffling
        public Deck Deck = new Deck(false);    // Deck in play after shuffle
        public List<Deck> NewDecks = new List<Deck>();    // Card decks not in play       
        //public List<Card> BonePile = new List<Card>();    // Cards not in play
        public readonly int NumberOfDecks = 2;    // Total number of decks to have in the game environment
        //public List<Player> Seats = new List<Player>();
        private List<Player> _seats;
        public List<Player> Seats
        {
            get { return _seats; }
            set
            {
                _seats = value;
                OnPropertyChanged(nameof(Seats));
            }
        }

        // Environment is like a virtual game table.  Card decks and players are initialized here.
        public Environment()
        {
            //Setup();
        }

        public void Setup()
        {
            EnableDeal = false;
            Seats = new List<Player>();            
            NewDecks.Clear();
            GetDecks();
            PutDecksInPlay();            
            CallShuffle();
            Dealer = new Player("Dealer", true);
            Seats.Add(Dealer);
            Player = new Player("Player 1");
            Seats.Add(Player);
            Deal();
        }

        private void CallShuffle()
        {
            Speak("Shuffling.");
            Deck.SyncShuffle();
            //Deck.Shuffle();
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

        public void Speak(string output)
        {
            //Console.WriteLine($"[{GetTime()}] : {output}");
            Output = $"[{GetTime()}] : {output}";
        }

        public string GetTime()
        {
            DateTime now = DateTime.Now;
            string time = now.ToString("HH:mm:ss");
            return time;
        }

        public void Deal()
        {
            Dealer.ClearHand();
            Player.ClearHand();
            for (int i = 0; i < 2; i++)
            {
                foreach (Player player in Seats)
                {
                    Hit(player);
                }
            }
            foreach (Player player in Seats)
            {
                
            }
            EnableDeal = false;
            EnableTurn = true;
        }

        public void PlayerHit()
        {
            Hit(Player, true, true);
            if (Player.Busted)
            {
                EnableDeal = true;
            }
        }

        public void PlayerStand()
        {
            Stand(Player);
            DealerTurn();
            EnableTurn = false;
        }

        private void CheckWinner()
        {
            if (Player.HandValue > Dealer.HandValue)
            {
                Player.Wins++;
                Dealer.Losses++;
                Speak($"{Player.Name} WINS!");
                EnableDeal = true;
            }
            else if (Dealer.HandValue > Player.HandValue)
            {
                Dealer.Wins++;
                Player.Losses++;
                Speak($"{Dealer.Name} WINS!");
                EnableDeal = true;
            }
            // House wins by default
            else
            {
                Dealer.Wins++;
                Player.Losses++;
                Speak($"{Dealer.Name} WINS!");
                EnableDeal = true;
            }
        }

        private void Stand(Player player)
        {
            Speak($"{player.Name} stands!");
            player.Stand = true;
            if((Dealer.Stand == true) & (Player.Stand == true))
            {
                CheckWinner();
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
            Speak($"{player.Name} hits!");
            Card card = Deck.GetCard();
            player.GiveCard(card);
            if (announce)
            {
                
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
                EnableTurn = false;
                player.Busted = true;
                Speak($"{player.Name} BUSTED!");
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
            Speak("Dealer's turn.");
            EnableTurn = false;
            while (Dealer.HandValue < 17)
            {
                Hit(Dealer, true, true);
            }
            if ((Dealer.HandValue >= 17) & (Dealer.Busted == false))
            {
                Stand(Dealer);
            }
            if (Dealer.Busted)
            {
            }
            EnableDeal = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
