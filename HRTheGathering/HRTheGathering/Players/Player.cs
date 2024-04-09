using HRTheGathering.Cards;
using HRTheGathering.Observers;
using HRTheGathering.Publishers;

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

        public void UseCard(Card card, Publisher publisher, Stack<Card>? stack = null)
        {
            List<Card> newCardsOnBoard = new List<Card>(CardsOnBoard);
            newCardsOnBoard.Add(card);
            CardsOnBoard = newCardsOnBoard;

            List<Card> newHand = new List<Card>(Hand);
            newHand.Remove(card);
            Hand = newHand; 

            if (card is CreatureCard)
            {
                CreatureCard creature = (CreatureCard)card;
                publisher.SubscribeChangeStats(creature, this);

                if (creature.CardEffect != null)
                {
                    creature.CardEffect.ApplyEffect();
                }
            }

            if (card is SpellCard or InstantCard)
            {
                Console.WriteLine("hello");
                if (stack != null)
                {
                    stack.Push(card);
                }
            }
        }

        public void UseCardWithCost(Card card, Publisher publisher, Stack<Card>? stack = null)
        {
            // If not enough Lands to play the card, return out
            if (CardsOnBoard.Count(land => land is LandCard && ((LandCard)land).CardColor == card.CardColor && !((LandCard)land).IsTurned) < card.Cost)
            {
                return;
            }
            
            // Turn lands until cost
            int landsTurned = 0;
            foreach (var landCard in CardsOnBoard)
            {
                if (card != null && landCard is LandCard land && land.CardColor == card.CardColor && !land.IsTurned)
                {
                    land.IsTurned = true;
                    landsTurned++;

                    if (landsTurned == card.Cost)
                    {
                        // We have turned enough lands, break out of the loop
                        break;
                    }
                }
            }

            if (card is SpellCard or InstantCard)
            {
                UseCard(card, publisher, stack);
            }
            else
            {
                UseCard(card, publisher);
            }
        }

        public void DiscardCard(Card card, Publisher publisher)
        {
            // Add check if its on the board or in the hand
            if (Hand.Contains(card))
            {
                List<Card> newHand = new List<Card>(Hand);
                newHand.Remove(card);
                Hand = newHand;
            }
            else
            {
                CardsOnBoard.Remove(card);
            }

            DiscardPile.Add(card);

            if (card is CreatureCard)
            {
                publisher.UnsubscribeRemovedCreatureCard(card);
            }
        }

        public void ChangeCardsInHandState(int amountCards, Player player, Publisher publisher)
        {
            // Remove Cards if amount < 0
            if (amountCards < 0)
            {
                Random random = new Random();
                for (int x = 0; x < -amountCards; x++)
                {
                    int randomIndex = random.Next(0, player.Hand.Count());
                    Card cardToDelete = player.Hand[randomIndex];
                    player.DiscardCard(cardToDelete, publisher);
                }
            }

            // Add Cards if amount > 0
            else if (amountCards > 0)
            {
                for (int y = 0; y < amountCards; y++)
                {
                    player.DrawCard();
                }
            }

            return;
        }

        public bool HasBlockAttackInstant()
        {
            foreach (Card card in Hand)
            {
                if (card is SpellCard && card.Name == "BlockAttackInstant")
                {
                    return true;
                }
            }
            return false;
        }

        public bool HasBlockInstantInstant()
        {
            foreach (Card card in Hand)
            {
                if (card is SpellCard && card.Name == "BlockInstantInstant")
                {
                    return true;
                }
            }
            return false;
        }
    }
}
