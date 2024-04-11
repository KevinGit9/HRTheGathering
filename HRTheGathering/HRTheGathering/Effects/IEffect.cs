using System;

namespace HRTheGathering.Effects
{
    public interface IEffect
    {
        string Description { get; }
        void ApplyEffect();
    }
}
