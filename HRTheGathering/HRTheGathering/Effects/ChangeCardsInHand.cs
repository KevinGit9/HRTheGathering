using HRTheGathering.Players;
using HRTheGathering.Publishers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRTheGathering.Effects
{
    public class ChangeCardsInHand : IEffect
    {
        private int amountCards;
        private Player playerTarget;
        private Publisher publisher;

        public ChangeCardsInHand(int amount, Player player, Publisher publisherTarget)
        {
            amountCards = amount;
            playerTarget = player;
            publisher = publisherTarget;
        }

        public void ApplyEffect()
        {
            publisher.ChangeStateCardsInHand(amountCards, playerTarget, publisher);
        }
    }
}
