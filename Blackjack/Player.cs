using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    class Player
    {
        private int ID;
        private int Money;
        private int BettedMoney;
        private List<int> PlayerHand;
        private bool PlayStatus;

        public Player()
        {
            PlayerHand = new List<int>();
            Money = 100;
            BettedMoney = 0;
            PlayStatus = true;
        }

        public Player(int ID)
        {
            this.ID = ID;
            PlayerHand = new List<int>();
            Money = 100;
            BettedMoney = 0;
            PlayStatus = true;
        }

        public bool PlayStatusProp { get { return PlayStatus; } set { PlayStatus = value; } }
        public int BettedMoneyProp { get { return BettedMoney; } }
        public List<int> ShowplayerHandProp { get { return PlayerHand; } }
        public int ShowID { get { return ID; } }

        public void ReceiveCard(int card)
        {
            PlayerHand.Add(card);
        }

        public void ShowHand()
        {
            if (PlayerHand.Count <= 0)
            {
                Console.WriteLine("Your hand is empty");
            }
            else
            {
                foreach (int card in PlayerHand)
                {
                    Console.WriteLine(card);
                }
            }
        }
        public int CountCards()
        {
            return PlayerHand.Sum();
        }

        public int BetMoney()
        {
            Console.WriteLine($"How much money do you want to bet? You have a total of: {Money}");
            int OutgoingMoney = int.Parse(Console.ReadLine());

            Money -= OutgoingMoney;
            BettedMoney = OutgoingMoney;
            Console.WriteLine($"You betted: {BettedMoney}");
            return BettedMoney;
        }

        public void AddMoney(int WonMoney)
        {
            Money += WonMoney * 2;
            ResetBetMoney();  
        }

        public void ResetBetMoney()
        {
            BettedMoney = 0;
        }

        public void EmptyHand()
        {
            PlayerHand.Clear();
        }
    }
}
