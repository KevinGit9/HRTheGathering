using HRTheGathering.Cards;
using HRTheGathering.Observers;

namespace HRTheGathering.Players
{
    public class Player
    {
        private int health = 10;
        private List<Card> hand = new List<Card>();
        private List<Card> board = new List<Card>();
        private Observable<int> healthObservable = new Observable<int>();
        private Observable<List<Card>> handObservable = new Observable<List<Card>>();
        private Observable<List<Card>> boardObservable = new Observable<List<Card>>();

        public int Id { get; set; }
        public string? Name { get; set; }

        public int Health
        {
            get { return health; }
            set
            {
                if (value != health)
                {
                    health = value;
                    healthObservable.NotifyObservers(value);
                }
            }
        }

        public List<Card> Hand
        {
            get { return hand; }
            set
            {
                hand = value;
                handObservable.NotifyObservers(value);
            }
        }

        public List<Card> CardsOnBoard
        {
            get { return board; }
            set
            {
                board = value;
                boardObservable.NotifyObservers(value);
            }
        }

        public List<Card> Deck { get; set; } = new List<Card>(); // Deck has 30 cards
        public List<Card> DiscardPile { get; set; } = new List<Card>(); // Graveyard

        // Observable properties
        public Observable<int> HealthObservable => healthObservable;
        public Observable<List<Card>> HandObservable => handObservable;
        public Observable<List<Card>> BoardObservable => boardObservable;

        public int MaxCardsInHand = 7;


        // Game methods
        public void ShuffleDeck()
        {
            Random rng = new Random();
            int n = Deck.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                Card value = Deck[k];
                Deck[k] = Deck[n];
                Deck[n] = value;
            }
        }

        public bool DrawCard()
        {
            // if Deck is empty return false to declare a winner
            if (Deck.Count == 0)
            {
                return false;
            }

            // Remove the top card from the Deck and add it to the Hand
            Card topCard = Deck[0];
            Deck.RemoveAt(0);

            List<Card> newHand = new List<Card>(Hand);
            newHand.Add(topCard);
            Hand = newHand;

            return true;
        }

        public void UseCard(Card card)
        {
            List<Card> newCardsOnBoard = new List<Card>(CardsOnBoard);
            newCardsOnBoard.Add(card);
            CardsOnBoard = newCardsOnBoard;

            List<Card> newHand = new List<Card>(Hand);
            newHand.Remove(card);
            Hand = newHand; 
        }

        public void DiscardCard(Card card)
        {
            // Add check if its on the board or in the hand
            this.CardsOnBoard.Remove(card);
            this.DiscardPile.Add(card);
        }
    }
}
