using System;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using HRTheGathering.Cards;
using HRTheGathering.Players;

namespace HRTheGathering.Publishers
{
    public delegate void CreatureStatChangeHandler(int attack, int defense);
    public delegate void CardsInHandChangeHandler(int amountCards, Player player);

    public class Publisher
    {
        public event CreatureStatChangeHandler CreatureStatChangeEventPlayer1; 
        public event CardsInHandChangeHandler CardsInHandStateChangeEventPlayer1;

        public event CreatureStatChangeHandler CreatureStatChangeEventPlayer2;
        public event CardsInHandChangeHandler CardsInHandStateChangeEventPlayer2;

        // Creature Stats Change
        public void ChangeStatsCreatures(int attack, int defense, Player player)
        {
            if (player.Id == 1)
            {
                CreatureStatChangeEventPlayer1?.Invoke(attack, defense);
            }
            else if (player.Id == 2)
            {
                CreatureStatChangeEventPlayer2?.Invoke(attack, defense);
            }
        }

        public void SubscribeChangeStats(CreatureCard creature, Player player)
        {
            if (player.Id == 1)
            {
                CreatureStatChangeEventPlayer1 += creature.ChangeStats;
            }
            else if (player.Id == 2)
            {
                CreatureStatChangeEventPlayer2 += creature.ChangeStats;
            }
        }

        public void UnsubscribeChangeStats(CreatureCard creature, Player player)
        {
            if (player.Id == 1)
            {
                CreatureStatChangeEventPlayer1 -= creature.ChangeStats;
            }
            else if (player.Id == 2)
            {
                CreatureStatChangeEventPlayer2 -= creature.ChangeStats;
            }
        }

        // CardsInHand State Change 
        public void ChangeStateCardsInHand(int amountCards, Player player)
        {
            if (player.Id == 1)
            {
                CardsInHandStateChangeEventPlayer1?.Invoke(amountCards, player);
            }
            else if (player.Id == 2)
            {
                CardsInHandStateChangeEventPlayer2?.Invoke(amountCards, player);
            }
        }

        public void SubscribeCardsInHand(Player player)
        {
            if (player.Id == 1)
            {
                CardsInHandStateChangeEventPlayer1 += player.ChangeCardsInHandState;
            }
            else if (player.Id == 2)
            {
                CardsInHandStateChangeEventPlayer2 += player.ChangeCardsInHandState;
            }
        }

        public void UnsubscribeCardsInHand(Player player)
        {
            if (player.Id == 1)
            {
                CardsInHandStateChangeEventPlayer1 -= player.ChangeCardsInHandState;
            }
            else if (player.Id == 2)
            {
                CardsInHandStateChangeEventPlayer2 -= player.ChangeCardsInHandState;
            }
        }
    }
}
