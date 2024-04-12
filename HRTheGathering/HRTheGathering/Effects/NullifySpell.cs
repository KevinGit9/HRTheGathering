using System;

namespace HRTheGathering.Effects
{
    public class NullifySpell : IEffect
    {
        public string Description { get; }
        public int? Duration { get; set; }

        public NullifySpell(string description)
        {
            Description = description;
        }

        public void ApplyEffect()
        {
            return;
        }
    }
}
