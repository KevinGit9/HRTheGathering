using HRTheGathering.Cards;
using System.ComponentModel.DataAnnotations;

namespace HRTheGathering.Players
{
    public class Player
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int HP { get; set; } = 10;
        public List<Card> Deck { get; set; } = new List<Card>();
        public List<Card> DiscardPile { get; set; } = new List<Card>();
        public List<Card> CardsOnBoard { get; set; } = new List<Card>();
        public List<Card> CardsInHand
        {
            get
            {
                return CardsInHand;
            }
            set
            {
                if (value.Count <= 7)
                {
                    CardsInHand = value;
                }
                else
                {
                    // Add function so player can discard a card.
                }
            }
        }
            
    }
}
