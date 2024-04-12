using System;

namespace HRTheGathering.Effects
{
    public interface IEffect
    {
        string Description { get; }
        int? Duration { get; set; }
        void ApplyEffect();

        void DecrementDuration()
        {
            Duration--;
        }

        bool IsExpired()
        {
            return Duration <= 0;
        }
    }
}
