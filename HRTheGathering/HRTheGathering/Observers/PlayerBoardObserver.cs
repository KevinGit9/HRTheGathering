using System;
using HRTheGathering.Cards;

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
                else if (card is SpellCard)
                {
                    // Cast card to SpellCard
                    SpellCard spellCard = (SpellCard)card;
                    Console.WriteLine($"[{card.CardColor} Spell][{card.Cost}] {card.Name} ({spellCard.CardEffect.Description})");
                }
                else if (card is InstantCard)
                {
                    // Cast card to InstantCard
                    InstantCard instantCard = (InstantCard)card;
                    Console.WriteLine($"[{card.CardColor} Instant][{card.Cost}] {card.Name} ({instantCard.CardEffect.Description})");
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
