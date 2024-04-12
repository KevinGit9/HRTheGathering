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
        public int? Duration { get; set; }
        public string Description { get; }

        public ChangeStats(int attack, int defense, Player player, Publisher publisherTarget, string description, int? duration = null)
        {
            attackChange = attack;
            defenseChange = defense;
            playerTarget = player;
            publisher = publisherTarget;
            Description = description;
            Duration = duration;
        }

        public void ApplyEffect()
        {
            // apply the stat changes
            publisher.ChangeStatsCreatures(attackChange, defenseChange, playerTarget);
        }

        public void RevertEffect()
        {
            // revert the stat changes
            publisher.ChangeStatsCreatures(-attackChange, -defenseChange, playerTarget);
        }

        public bool IsExpired() 
        {
            bool isExpired = Duration <= 0;

            if (isExpired)
            {
                RevertEffect();
            }

            return isExpired;
        }
    }
}
