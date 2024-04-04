using HRTheGathering.Cards;
using HRTheGathering.Observers;

namespace HRTheGathering.Players
{
    public class Player
    {
        private int health = 10;
        private List<Card> hand = new List<Card>();
        private Observable<int> healthObservable = new Observable<int>();
        private Observable<List<Card>> handObservable = new Observable<List<Card>>();

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
                Console.WriteLine("hello");
                handObservable.NotifyObservers(value);
            }
        }
    
        public List<Card> Deck { get; set; } = new List<Card>(); // Deck has 30 cards
        public List<Card> DiscardPile { get; set; } = new List<Card>(); // Graveyard
        public List<Card> CardsOnBoard { get; set; } = new List<Card>(); // Board

        // Observable properties
        public Observable<int> HealthObservable => healthObservable;
        public Observable<List<Card>> HandObservable => handObservable;

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
            Hand = Hand.Concat(new[] { topCard }).ToList();

            return true;
        }

        public void UseCard(Card card)
        {
            this.CardsOnBoard.Add(card);
            this.Hand.Remove(card);
        }

        public void DiscardCard(Card card)
        {
            this.DiscardPile.Add(card);
            this.CardsOnBoard.Remove(card);
        }
    }
}
