using HRTheGathering.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                if (card.CardType == Type.Creature)
                {
                    CreatureCard creatureCard = (CreatureCard) card;
                    creatureCard.IsAttacker = false;
                    creatureCard.IsDefender = false;
                }
            }
        }

        public CreatureCard GetAttackerCreatureCard(List<Card> playerCards)
        {
            foreach (Card card in playerCards)
            {
                if (card.CardType == Type.Creature)
                {
                    CreatureCard creatureCard = (CreatureCard) card;
                    if (creatureCard.IsAttacker == true)
                    {
                        return creatureCard;
                    }
                }
            }
            return null;
        }

        public CreatureCard GetDefenderCreatureCard(List<Card> playerCards)
        {
            foreach (Card card in playerCards)
            {
                if (card.CardType == Type.Creature)
                {
                    CreatureCard creatureCard = (CreatureCard)card;
                    if (creatureCard.IsDefender == true)
                    {
                        return creatureCard;
                    }
                }
            }
            return null;
        }

        public void AttackEnemy(Player attackingPlayer, Player defendingPlayer)
        {
            // TODO: Player can choose to assign defender
            CreatureCard attacker = GetAttackerCreatureCard(attackingPlayer.CardsOnBoard);
            CreatureCard defender = GetDefenderCreatureCard(defendingPlayer.CardsOnBoard);

            if (attacker != null && defender == null)
            {
                defendingPlayer.HP -= attacker.Attack;
            }
            else if (attacker != null && defender != null)
            {
                defender.Defense -= attacker.Attack;
                attacker.Defense -= defender.Attack;
            }
        }
    }
}
