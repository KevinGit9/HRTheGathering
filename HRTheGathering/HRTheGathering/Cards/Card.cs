using HRTheGathering.Effects;

namespace HRTheGathering.Cards
{
    public abstract class Card
    {
        int Id { get; set; }
        public string Name { get; set; }
        int Cost { get; set; }
        public enum Type
        {
            Land,
            Spell,
            Permanent,
            Instant
        } // Make it be limited to Land, Spell (Spell can be a permanent that can attack or be instant or generate a permanent)
        IEffect CardEffect { get; set; }
        enum Color
        {
            Red,
            Blue,
            Brown,
            White,
            Green
        }
        public int CopiesInDeck { get; set; }

    }
}