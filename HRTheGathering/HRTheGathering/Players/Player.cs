using System.Collections.Generic;
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
                handObservable.NotifyObservers(value);
            }
        }

        public List<Card> Deck { get; set; } = new List<Card>(); // Deck has 30 cards
        public List<Card> DiscardPile { get; set; } = new List<Card>(); // Graveyard
        public List<Card> CardsOnBoard { get; set; } = new List<Card>(); // Board

        // Observable properties
        public Observable<int> HealthObservable => healthObservable;
        public Observable<List<Card>> HandObservable => handObservable;


        // Game methods
        public void DrawCard()
        {
            var factory = new CardFactory();
            Card card = factory.CreateLandCard();
            Hand = Hand.Concat(new[] { card }).ToList();
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
