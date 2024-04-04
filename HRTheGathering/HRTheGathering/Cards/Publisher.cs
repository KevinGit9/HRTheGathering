using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRTheGathering.Cards
{
    public delegate void DefenseDecreaseHandler(int amount);
    public delegate void AttackDecreaseHandler(int amount);

    public class Publisher
    {
        public event DefenseDecreaseHandler DecreaseDefenseEvent;
        public event AttackDecreaseHandler DecreaseAttackEvent;

        // Defense Decrease
        public void DecreaseDefenseOfSubscribedCreatures(int amount)
        {
            DecreaseDefenseEvent?.Invoke(amount);
        }

        public void SubscribeDefenseDecrease(CreatureCard creature)
        {
            DecreaseDefenseEvent += creature.DecreaseDefense;
        }

        public void UnsubscribeDefenseDecrease(CreatureCard creature)
        {
            DecreaseDefenseEvent -= creature.DecreaseDefense;
        }

        // Attack Decrease
        public void DecreaseAttackOfSubscribedCreatures(int amount)
        {
            DecreaseAttackEvent?.Invoke(amount);
        }

        public void SubscribeAttackDecrease(CreatureCard creature)
        {
            DecreaseAttackEvent += creature.DecreaseAttack;
        }

        public void UnsubscribeAttackDecrease(CreatureCard creature)
        {
            DecreaseAttackEvent -= creature.DecreaseAttack;
        }
    }
}
