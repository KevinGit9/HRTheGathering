using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HRTheGathering.Cards.Card;

namespace HRTheGathering.Cards
{
    public class CardFactory
    {
        public LandCard CreateLandCard(string name, Color color) 
        {
            return new LandCard { Name = name, Cost = 0, CardColor = color };
        }

        public CreatureCard CreateCreatureCard(string name, int cost, Color color, int attack, int defense)
        {
            //if (name == null)
            //{
                //Random random = new Random();
                //int length = random.Next(5, 11);
                //const string chars = "abcdefghijklmnopqrstuvwxyz";
                //string randomString = new string(Enumerable.Repeat(chars, length)
                //    .Select(s => s[random.Next(s.Length)]).ToArray());

                //string name = char.ToUpper(randomString[0]) + randomString.Substring(1);
            //}

            return new CreatureCard { Name = name, Cost = cost, CardColor = color, Attack = attack, Defense = defense};
        }

        public SpellCard CreateSpellCard()
        {
            return new SpellCard { };

        }

        public InstantCard CreateInstantCard()
        {
            return new InstantCard { };

        }

        public List<Card> CreateDeck(Color color)
        {
            List<Card> deck = new List<Card>();
            int maxCopiesPerCard = 3;

            // White Cards
            // Lands: Sunlit Meadows, Radiant Glade, Luminous Plains, Celestial Grove
            // Creatures: 1 cost Dawn Paladin (1,2), 2 cost Griffon (2,3), 3 cost Aegis Angel (4,4)
            // Spells:
            // - Divine Reprieve (Instant) 1 cost: Nullify the opponents attack.
            // - Radiant Blessing 3 cost: Gives all your creatures in the field +2/+2 until the end of the turn
            // - Celestial Purge 5 cost: Discards target creature


            // Red Cards
            // Lands: Molten Peak, Ember Highlands, Volcanic Crater, Blaze Ridge
            // Creatures: 1 cost Magma Hound (2,1), 2 cost Inferno Adept (2,2), 4 cost Ancient Dragon (6,6)
            // Spells:
            // - Volcanic Fury 1 cost: Target creature gets +3/0 until the end of the turn
            // - Flame Burst 2 cost (Instant): Deal 3 damage to target creature or player
            // - Emberforged Enhancement 2 cost: Target creature gets +1/+1 until it leaves the battlefield

            if (color == Color.White)
            {
                for (int i = 0; i < maxCopiesPerCard; i++)
                {
                    // Add white lands
                    deck.Add(CreateLandCard("Sunlit Meadows", Card.Color.White));
                    deck.Add(CreateLandCard("Radiant Glade", Card.Color.White));
                    deck.Add(CreateLandCard("Luminous Plains", Card.Color.White));
                    deck.Add(CreateLandCard("Celestial Grove", Card.Color.White));


                    // Add white creatures
                    deck.Add(CreateCreatureCard("Dawn Paladin", 1, Card.Color.White, 1, 2));
                    deck.Add(CreateCreatureCard("Griffon", 2, Card.Color.White, 2, 3));
                    deck.Add(CreateCreatureCard("Aegis Angel", 3, Card.Color.White, 4, 4));

                    // Add white spells
                    //deck.Add(CreateInstantCard("Divine Reprieve", Card.Color.White, 1, "Nullify the opponent's attack."));
                    //deck.Add(CreateSpellCard("Radiant Blessing", Card.Color.White, 3, "Gives all your creatures +2/+2 until the end of the turn."));
                    //deck.Add(CreateSpellCard("Celestial Purge", Card.Color.White, 5, "Discards target creature."));
                }
            }
            else if (color == Color.Red)
            {
                for (int i = 0; i < maxCopiesPerCard; i++)
                {
                    // Add red lands
                    deck.Add(CreateLandCard("Molten Peak", Card.Color.Red));
                    deck.Add(CreateLandCard("Ember Highlands", Card.Color.Red));
                    deck.Add(CreateLandCard("Volcanic Crater", Card.Color.Red));
                    deck.Add(CreateLandCard("Blaze Ridge", Card.Color.Red));


                    // Add red creatures
                    deck.Add(CreateCreatureCard("Magma Hound", 1, Card.Color.Red, 2, 1));
                    deck.Add(CreateCreatureCard("Inferno Adept", 2, Card.Color.Red, 2, 2));
                    deck.Add(CreateCreatureCard("Ancient Dragon", 4, Card.Color.Red, 6, 6));

                    // Add red spells
                    //deck.Add(CreateSpellCard("Volcanic Fury", Card.Color.Red, 1, "Target creature gets +3/0 until the end of the turn."));
                    //deck.Add(CreateInstantCard("Flame Burst", Card.Color.Red, 2, " Deal 3 damage to target creature or player."));
                    //deck.Add(CreateSpellCard("Emberforged Enhancement", Card.Color.Red, 2, "Target creature gets +1/+1 until it leaves the battlefield."));
                }
            }

            return deck;
        }
    }
}
