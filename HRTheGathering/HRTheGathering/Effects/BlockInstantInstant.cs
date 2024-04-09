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
        public string Description { get; }


        public BlockInstantInstant(Player player, string description)
        {
            playerTarget = player;
            Description = description;
        }

        public void ApplyEffect()
        {
            playerTarget.HasBlockInstantInstant();
        }
    }
}
