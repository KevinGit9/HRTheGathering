using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRTheGathering.Cards
{
    public class CardFactory
    {
        public Card CreateCard(string cardType)
        {
            switch (cardType)
            {
                case "Land":
                    return new LandCard { Cost = 0, IsTurned = false };
                case "Creature":
                    return new CreatureCard { IsAttacker = false, IsDefender = false };
                case "Spell":
                    return new SpellCard { };
                case "Instant":
                    return new InstantCard { };
                default:
                    throw new ArgumentException("Invalid card type");
            }
        }
    }
}
