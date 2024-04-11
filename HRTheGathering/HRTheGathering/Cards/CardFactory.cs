using System;
using HRTheGathering.Effects;
using HRTheGathering.Players;
using HRTheGathering.Publishers;
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
            return new CreatureCard { Name = name, Cost = cost, CardColor = color, Attack = attack, Defense = defense};
        }

        public SpellCard CreateSpellCard(string name, int cost, Color color, IEffect effect)
        {
            return new SpellCard { Name = name, Cost = cost, CardColor = color, CardEffect = effect };
        }

        public InstantCard CreateInstantCard(string name, int cost, Color color, IEffect effect)
        {
            return new InstantCard { Name = name, Cost = cost, CardColor = color, CardEffect = effect };
        }

        public List<Card> CreateDeck(Color color, Player player, Player enemyPlayer, Publisher publisher)
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
                    deck.Add(CreateLandCard("Sunlit Meadows", Color.White));
                    deck.Add(CreateLandCard("Radiant Glade", Color.White));
                    deck.Add(CreateLandCard("Luminous Plains", Color.White));
                    deck.Add(CreateLandCard("Celestial Grove", Color.White));


                    // Add white creatures
                    deck.Add(CreateCreatureCard("Dawn Paladin", 1, Color.White, 1, 2));
                    deck.Add(CreateCreatureCard("Griffon", 2, Color.White, 2, 3));
                    deck.Add(CreateCreatureCard("Aegis Angel", 3, Color.White, 4, 4));

                    // Add white spells
                    NullifySpell nullifySpellWhite = new NullifySpell("Nullify the opponents spell.");
                    deck.Add(CreateInstantCard("Divine Reprieve", 1, Color.White, nullifySpellWhite));
                    ChangeStats changeStats2 = new ChangeStats(2, 2, player, publisher, "Increases all your creatures stats by +2/+2.");
                    deck.Add(CreateSpellCard("Radiant Blessing", 3, Color.White, changeStats2));
                    ChangeCardsInHand discardCard1 = new ChangeCardsInHand(-1, enemyPlayer, publisher, "Discard 1 random cards of the opponents hand.");
                    deck.Add(CreateSpellCard("Celestial Purge", 3, Color.White, discardCard1));
                }
            }
            else if (color == Color.Red)
            {
                for (int i = 0; i < maxCopiesPerCard; i++)
                {
                    // Add red lands
                    deck.Add(CreateLandCard("Molten Peak", Color.Red));
                    deck.Add(CreateLandCard("Ember Highlands", Color.Red));
                    deck.Add(CreateLandCard("Volcanic Crater", Color.Red));
                    deck.Add(CreateLandCard("Blaze Ridge", Color.Red));


                    // Add red creatures
                    deck.Add(CreateCreatureCard("Magma Hound", 1, Color.Red, 2, 1));
                    deck.Add(CreateCreatureCard("Inferno Adept", 2, Color.Red, 2, 2));
                    deck.Add(CreateCreatureCard("Ancient Dragon", 4, Color.Red, 6, 6));

                    // Add red spells
                    NullifySpell nullifySpellRed = new NullifySpell("Nullify the opponents spell.");
                    deck.Add(CreateInstantCard("Flame Burst", 1, Color.Red, nullifySpellRed));
                    ChangeStats changeStatsEnemy2 = new ChangeStats(-1, -1, enemyPlayer, publisher, "Reduces stats of all opponent creatures by -1/-1.");
                    deck.Add(CreateSpellCard("Emberforged Enhancement", 2, Color.Red, changeStatsEnemy2));
                    ChangeCardsInHand addCards2 = new ChangeCardsInHand(2, player, publisher, "Draw 2 cards.");
                    deck.Add(CreateSpellCard("Volcanic Fury", 2, Color.Red, addCards2));
                }
            }

            return deck;
        }
    }

    // TODO: Add method to replicate the turns from the document
}
