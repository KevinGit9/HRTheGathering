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
        
        public LandCard CreateLandCard() 
        {
            Random random = new Random();

            // Generate a random name
            int length = random.Next(5, 11);
            const string chars = "abcdefghijklmnopqrstuvwxyz";
            string randomString = new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());

            string randomName = char.ToUpper(randomString[0]) + randomString.Substring(1);

            // Get a random color
            Color randomColor = (Color)random.Next(Enum.GetValues(typeof(Color)).Length);
            
            // Create a new Land Card using the random generated values
            return new LandCard { Name = randomName, Cost = 0, CardColor = randomColor, IsTurned = false };
        }

        public Card CreateCreatureCard()
        {
            return new CreatureCard { };

        }
        public Card CreateSpellCard()
        {
            return new SpellCard { };

        }

        public Card CreateInstantCard()
        {
            return new InstantCard { };

        }

        public List<Card> CreateDeck(int numberOfLands = 12, int numberOfCreatures = 10, int numberOfSpells = 8)
        {
            List<Card> deck = new List<Card>();

            for (int i = 0; i < numberOfLands; i++)
            {
                deck.Add(CreateLandCard());
            }

            for (int i = 0; i < numberOfCreatures; i++)
            {
                deck.Add(CreateCreatureCard());
            }

            for (int i = 0; i < numberOfSpells; i++)
            {
                if (i % 2 == 0)
                    // Add Spell Card
                    deck.Add(CreateSpellCard());
                else
                    // Add Instant Card
                    deck.Add(CreateInstantCard());
            }

            return deck;
        }
    }
}
