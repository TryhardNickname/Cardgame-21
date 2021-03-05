using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    class MainLoop
    {
        Deck deck;
        List<Player> ListOfPlayers;
        Dealer dealer = new Dealer();
        int AmountOfDecks;

        public void Initiate()
        {
            
            Console.WriteLine("Welcome");
            Console.WriteLine("Enter amount of decks: ");
            AmountOfDecks = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter amount of players: ");
            int AmountOfPlayers = int.Parse(Console.ReadLine());

            deck = new Deck(AmountOfDecks);
            ListOfPlayers = new List<Player>();
            
            for (int i = 0; i < AmountOfPlayers; i++)
            {
                ListOfPlayers.Add(new Player(i + 1));
            }
            GameLoop();
        }
        public void GameLoop()
        {
            while (true)
            {
                PlayerLoop();
                DealerLoop();

                foreach (Player player in ListOfPlayers)
                {
                    player.PlayStatusProp = true;
                    player.EmptyHand();
                    dealer.EmptyHand();
                }
                deck = new Deck(AmountOfDecks);
            }

        }

        public void PlayerLoop()
        {
            foreach (Player player in ListOfPlayers)
            {
                int NewCard;
                int TurnNumber = 1;

                Console.WriteLine("----------------");
                Console.WriteLine($"Player #{player.ShowID} turn");
                Console.WriteLine("----------------");

                NewCard = deck.ChooseCard();
                player.ReceiveCard(NewCard);
                Console.WriteLine("This is your first card");
                player.ShowHand();
                player.BetMoney();

                do
                {
                    Console.WriteLine("This is your hand: ");
                    player.ShowHand();

                    Console.WriteLine("1. Add card to hand");
                    Console.WriteLine("2. End your turn");
                    if (player.ShowplayerHandProp.Contains(1) || player.ShowplayerHandProp.Contains(14))
                    {
                        //acelogic
                    }

                    int playerinput = int.Parse(Console.ReadLine());
                    if (playerinput == 1)
                    {
                        TurnNumber++;
                        NewCard = deck.ChooseCard();
                        player.ReceiveCard(NewCard);

                        if (player.CountCards() == 21 && TurnNumber == 2)
                        {
                            Console.WriteLine($"You got 21! and won {player.BettedMoneyProp * 2}");
                            player.PlayStatusProp = false;
                            player.AddMoney(player.BettedMoneyProp);

                            Console.ReadKey();
                            break;
                            
                        }
                    }
                    else if (playerinput == 2)
                    {
                        break;
                    }
                } while (player.CountCards() <= 21);

                if (player.CountCards() > 21)
                {
                    player.ShowHand();
                    player.PlayStatusProp = false;
                    Console.WriteLine($"You exceeded 21 by {player.CountCards() - 21} and lost {player.BettedMoneyProp}. Press any key to continue");
                    player.ResetBetMoney();
                    Console.ReadKey();
                }
            }        
        }

        public void DealerLoop()
        {
            int NewCard;

            Console.WriteLine("----------------");
            Console.WriteLine("Dealers turn");
            Console.WriteLine("----------------");

            int PlayerStatusCount = 0;
            foreach (Player player in ListOfPlayers)
            {
                if (player.PlayStatusProp == true)
                {
                    PlayerStatusCount++;
                    Console.WriteLine($"Player #{player.ShowID}s hand: {player.CountCards()}");
                }
            }

            if (PlayerStatusCount != 0)
            {
                do
                {
                    Console.WriteLine("This is your hand: ");
                    dealer.ShowHand();

                    Console.WriteLine("1. Add card to hand");
                    Console.WriteLine("2. End your turn");

                    int playerinput = int.Parse(Console.ReadLine());
                    if (playerinput == 1)
                    {
                        NewCard = deck.ChooseCard();
                        dealer.ReceiveCard(NewCard);
                    }
                    else if (playerinput == 2)
                    {
                        foreach (Player player in ListOfPlayers)
                        {
                            if (player.PlayStatusProp == true)
                            {
                                if (dealer.CountCards() >= player.CountCards())
                                {
                                    Console.WriteLine($"Dealer won vs Player #{player.ShowID}, player lost {player.BettedMoneyProp}.");
                                    dealer.AddMoney(player.BettedMoneyProp);
                                    player.ResetBetMoney();
                                }
                                else
                                {
                                    Console.WriteLine($"Player #{player.ShowID} won {player.BettedMoneyProp * 2} against Dealer.");
                                    player.AddMoney(player.BettedMoneyProp);
                                    player.ResetBetMoney();
                                }
                            }
                        }
                        Console.ReadKey();
                        break;

                    }
                } while (dealer.CountCards() < 21);

                if (dealer.CountCards() == 21)
                {
                    Console.WriteLine("Dealer received 21 and won everything!");
                    foreach (Player player in ListOfPlayers)
                    {
                        Console.WriteLine($"Player #{player.ShowID} lost {player.BettedMoneyProp}");
                        dealer.AddMoney(player.BettedMoneyProp);
                        player.ResetBetMoney();
                    }
                    Console.ReadKey();
                }
                else if (dealer.CountCards() > 21)
                {
                    dealer.ShowHand();
                    Console.WriteLine($"Dealer exceeded 21 by {dealer.CountCards() - 21}.");
                    foreach (Player player in ListOfPlayers)
                    {
                        if (player.PlayStatusProp == true)
                        {
                            Console.WriteLine($"Player #{player.ShowID} won {player.BettedMoneyProp * 2}");
                            player.AddMoney(player.BettedMoneyProp);
                            player.ResetBetMoney();
                        }
                    }
                    Console.ReadKey();
                }
            }
            else
            {
                Console.WriteLine("Every player lost, next turn!");
            }
              
        }
    }


}
