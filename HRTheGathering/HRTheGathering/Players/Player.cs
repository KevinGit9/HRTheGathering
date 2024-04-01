using HRTheGathering.Cards;
using HRTheGathering.Observers;
using System.ComponentModel.DataAnnotations;

namespace HRTheGathering.Players
{
    public class Player
    {
        private int health { get; set; } = 10;
        private List<Card> hand = new List<Card>();
        private List<IGameObserver<int>> healthObservers = new List<IGameObserver<int>>();
        private List<IGameObserver<List<Card>>> handObservers = new List<IGameObserver<List<Card>>>();

        public List<IGameObserver<int>> HealthObservers => healthObservers;
        public List<IGameObserver<List<Card>>> HandObservers => handObservers;


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
                    NotifyObservers(healthObservers, health);
                }
            }
        }

        public List<Card> Hand
        {
            get { return hand; }
            set
            {
                hand = value;
                NotifyObservers(handObservers, hand);
            }
        }
        public List<Card> Deck { get; set; } = new List<Card>();
        public List<Card> DiscardPile { get; set; } = new List<Card>(); // Graveyard
        public List<Card> CardsOnBoard { get; set; } = new List<Card>(); // Board


        // Game methods
        public Card DrawCard()
        {
            return new PermanentCard();
        }

        // Observer methods
        public void Attach<T>(IGameObserver<T> observer, List<IGameObserver<T>> observers)
        {
            observers.Add(observer);
        }

        public void Detach<T>(IGameObserver<T> observer, List<IGameObserver<T>> observers)
        {
            observers.Remove(observer);
        }

        private void NotifyObservers<T>(List<IGameObserver<T>> observers, T data)
        {
            foreach (var observer in observers)
            {
                observer.Update(data);
            }
        }

    }
}
