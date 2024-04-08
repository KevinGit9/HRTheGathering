using HRTheGathering.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRTheGathering.Effects
{
    public class BlockInstantInstant : IEffect
    {
        private Player playerTarget;

        public BlockInstantInstant(Player player)
        {
            playerTarget = player;
        }

        public void ApplyEffect()
        {
            playerTarget.HasBlockInstantInstant();
        }
    }
}
