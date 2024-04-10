using HRTheGathering.Players;

namespace HRTheGathering.Cards
{
    public class CreatureCard : Card
    {
        public int Attack { get; set; }
        public int Defense { get; set; }
        public bool IsTurned { get; set; } = false;

        public void ChangeStats(int attack, int defense)
        {
            Attack += attack;
            Defense += defense;
        }
    }
}
