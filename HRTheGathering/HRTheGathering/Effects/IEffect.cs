using HRTheGathering.Players;

namespace HRTheGathering.Effects
{
    public interface IEffect
    {
        void ApplyEffect(Player caster, object target);
    }
}
