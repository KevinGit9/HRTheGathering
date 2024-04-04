using HRTheGathering.Effects;

namespace HRTheGathering.Cards
{
    public abstract class Card
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Cost { get; set; }
        public int CopiesInDeck { get; set; }
        public IEffect? CardEffect { get; set; }
        public Type CardType { get; set; }
        public Color? CardColor { get; set; }

        public enum Type
        {
            Land,
            Creature,
            Spell,
            Instant
        } // Make it be limited to Land, Spell (Spell can be a permanent that can attack or be instant or generate a permanent)

        public enum Color
        {
            Red,
            Blue,
            Brown,
            White,
            Green
            // Implement colorless
        }

    }
}