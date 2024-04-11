using System;
using HRTheGathering.Players;
using HRTheGathering.Publishers;

namespace HRTheGathering.Effects
{
    public class ChangeStats : IEffect
    {
        private int attackChange;
        private int defenseChange;
        private Player playerTarget;
        private Publisher publisher;

        public string Description { get; }

        public ChangeStats(int attack, int defense, Player player, Publisher publisherTarget, string description)
        {
            attackChange = attack;
            defenseChange = defense;
            playerTarget = player;
            publisher = publisherTarget;
            Description = description;
        }

        public void ApplyEffect()
        {

            publisher.ChangeStatsCreatures(attackChange, defenseChange, playerTarget);
        }
    }
}
