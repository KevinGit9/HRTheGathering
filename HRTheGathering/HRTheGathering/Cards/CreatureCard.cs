using HRTheGathering.Players;

namespace HRTheGathering.Cards
{
    public class CreatureCard : Card
    {
        public int Attack { get; set; }
        public int Defense { get; set; }
        public bool IsAttacker { get; set; } = false;
        public bool IsDefender { get; set; } = false;

        public void SetAttacker(CreatureCard creatureCard)
        {
            creatureCard.IsAttacker = true;
        }

        public void SetDefender(CreatureCard creatureCard)
        {
            creatureCard.IsDefender = true;
        }

        public void ClearAllAttackerDefender(List<Card> playerCards)
        {
            foreach (Card card in playerCards)
            {
                if (card is CreatureCard)
                {
                    CreatureCard creatureCard = (CreatureCard) card;
                    creatureCard.IsAttacker = false;
                    creatureCard.IsDefender = false;
                }
            }
        }

        public void AttackEnemy(Player attackingPlayer, Player defendingPlayer)
        {
            // TODO: Player can choose to assign defender

            CreatureCard? attackingCreature = null;
            CreatureCard? defendingCreature = null;

            foreach (Card card in attackingPlayer.CardsOnBoard)
            {
                if (card is CreatureCard)
                {
                    CreatureCard creature = (CreatureCard)card;
                    if (attackingCreature == null)
                    {
                        attackingCreature = creature;
                    }
                    else if (creature.Attack > attackingCreature.Attack)
                    {
                        attackingCreature = creature;
                    }
                }
            }

            if (attackingCreature != null)
            {
                SetAttacker(attackingCreature);
            }

            foreach (Card card in defendingPlayer.CardsOnBoard)
            {
                if (card is CreatureCard)
                {
                    CreatureCard creature = (CreatureCard)card;
                    if (defendingCreature == null)
                    {
                        attackingCreature = creature;
                    }
                    else if (creature.Defense > defendingCreature.Defense)
                    {
                        defendingCreature = creature;
                    }

                }
            }

            if (defendingCreature != null)
            {
                SetDefender(defendingCreature);
            }

            if (attackingCreature != null)
            {
                if (!defendingPlayer.HasBlockAttackInstant())
                {
                    if (defendingCreature == null)
                    {
                        defendingPlayer.Health -= attackingCreature.Attack;
                    }
                    else if (defendingCreature != null)
                    {
                        defendingCreature.Defense -= attackingCreature.Attack;
                        attackingCreature.Defense -= defendingCreature.Attack;
                    }
                }
                else if (defendingPlayer.HasBlockAttackInstant())
                {
                    if (attackingPlayer.HasBlockInstantInstant())
                    {
                        if (defendingCreature == null)
                        {
                            defendingPlayer.Health -= attackingCreature.Attack;
                        }
                        else if (defendingCreature != null)
                        {
                            defendingCreature.Defense -= attackingCreature.Attack;
                            attackingCreature.Defense -= defendingCreature.Attack;
                        }
                    }
                }
            }

            ClearAllAttackerDefender(attackingPlayer.Hand);
            ClearAllAttackerDefender(defendingPlayer.Hand);
        }

        public void ChangeStats(int attack, int defense)
        {
            Attack += attack;
            Defense += defense;
        }
    }
}
