using HRTheGathering.Cards;
using HRTheGathering.Observers;
using System.ComponentModel.DataAnnotations;

namespace HRTheGathering.Players
{
    public class Player
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int HP { get; set; } = 10;
        public List<Card> Deck { get; set; } = new List<Card>();
        public List<Card> DiscardPile { get; set; } = new List<Card>(); // Graveyard
        public List<Card> CardsOnBoard { get; set; } = new List<Card>(); // Board
        private List<Card> _cardsInHand = new List<Card>(); // Field to store cards in hand
        public List<Card> CardsInHand // Hand
        {
            get
            {
                return _cardsInHand;
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
