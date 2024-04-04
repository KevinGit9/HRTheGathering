using HRTheGathering.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRTheGathering.Observers
{
    class PlayerBoardObserver : IGameObserver<List<Card>>
    {
        public void Update(List<Card> cardsOnField)
        {
            Console.WriteLine("Player field changed:");
            foreach (var card in cardsOnField)
            {
                if (card is CreatureCard)
                {
                    // Cast card to CreatureCard
                    CreatureCard creatureCard = (CreatureCard)card;
                    Console.WriteLine($"[{card.CardColor} Creature][{card.Cost}] {card.Name} ({creatureCard.Attack}, {creatureCard.Defense})");
                }
                else if (card is LandCard)
                {
                    Console.WriteLine($"[{card.CardColor} Land] {card.Name}");
                }
                else
                {
                    Console.WriteLine($"[{card.Cost}] {card.Name}");
                }

            }
            Console.WriteLine("\n");
        }
    }
}
