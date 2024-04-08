using HRTheGathering.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRTheGathering.Effects
{
    public class BlockAttackInstant : IEffect
    {
        private Player playerTarget;

        public BlockAttackInstant(Player player) 
        {
            playerTarget = player;
        }

        public void ApplyEffect()
        {
            playerTarget.HasBlockAttackInstant();
        }
    }
}
