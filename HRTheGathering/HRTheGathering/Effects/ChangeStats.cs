using HRTheGathering.Cards;

namespace HRTheGathering.Effects
{
    public class ChangeStats : IEffect
    {
        private int attackChange;
        private int defenseChange;

        public ChangeStats(int attack, int defense)
        {
            attackChange = attack;
            defenseChange = defense;
        }

        public void ApplyEffect(object target)
        {
            if (target is not CreatureCard) return;

            CreatureCard creature = (CreatureCard)target;

            creature.Attack += attackChange;
            creature.Defense += defenseChange;
        }
    }
}
