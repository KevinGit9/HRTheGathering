using System;
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
        public Color? CardColor { get; set; }

        public enum Color
        {
            Red,
            Blue,
            Brown,
            White,
            Green
            // TODO: Implement colorless
        }
    }
}