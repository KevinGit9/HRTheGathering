using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using HRTheGathering.Cards;
using HRTheGathering.Players;

namespace HRTheGathering.Publishers
{
    public delegate void StatChangeHandler(int attack, int defense); // is new
    public delegate void CardsInHandChangeHandler(int amount, Player player, Card card);

    public class Publisher
    {
        public event StatChangeHandler StatChangeEvent; // is new
        public event CardsInHandChangeHandler CardsInHandStateChangeEvent;

        public void ChangeStats(int attack, int defense)
        {
            StatChangeEvent?.Invoke(attack, defense);
        }
        public void SubscribeChangeStats(CreatureCard creature)
        {
            StatChangeEvent += creature.ChangeStats;
        }

        public void UnsubscribeChangeStats(CreatureCard creature)
        {
            StatChangeEvent -= creature.ChangeStats;
        }

        // CardHand Change
        //public void ChangeStateOfSubscribedCardsInHand(int amount, Player player, Card card)
        //{
        //    CardsInHandStateChangeEvent?.Invoke(amount, player, card);
        //}

        //public void SubscribeCardsInHandPlayer1(Card card)
        //{
        //    CardsInHandStateChangeEvent += card.ChangeCardInHandState;
        //}

        //public void UnsubscribeCardsInHandPlayer2(Card card)
        //{
        //    CardsInHandStateChangeEvent -= card.ChangeCardInHandState;
        //}
    }
}
