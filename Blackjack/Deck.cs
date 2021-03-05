using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    class Deck
    {
        private List<int> DeckOfCards;

        public Deck(int NumberOfDecks)
        {
            DeckOfCards = new List<int>();
            for (int h = 0; h < NumberOfDecks; h++)
            {
                for (int i = 1; i < 14; i++)
                {
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            DeckOfCards.Add(i);
                        }
                    }
                }
            }
        }
        public int ChooseCard()
        {
            int CardValue = 0;
            int RandomCardIndex;
            Random rand = new Random();
            do
            {
                RandomCardIndex = rand.Next(0, DeckOfCards.Count);
                if (DeckOfCards[RandomCardIndex] != 0)
                {
                    CardValue = DeckOfCards[RandomCardIndex];
                }
                
            } while (DeckOfCards[RandomCardIndex] == 0);
            
            DeckOfCards[RandomCardIndex] = 0;
            return CardValue;
        }

        public void PrintDeck()
        {
            foreach (int number in DeckOfCards)
            {
                Console.WriteLine(number);
            }
            Console.ReadKey();
        }
    }
}
