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
        public string Description { get; }

        public ChangeCardsInHand(int amount, Player player, Publisher publisherTarget, string description)
        {
            amountCards = amount;
            playerTarget = player;
            publisher = publisherTarget;
            Description = description;
        }

        public void ApplyEffect()
        {
            publisher.ChangeStateCardsInHand(amountCards, playerTarget, publisher);
        }
    }
}
