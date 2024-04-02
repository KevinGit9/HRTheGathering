using HRTheGathering.Cards;
using HRTheGathering.Observers;
using HRTheGathering.Players;

namespace HRTheGathering.Board
{
    class Board
    {
        private static Board instance = new Board();
        private int currentRound;
        private Player player1;
        private Player player2;
        private PlayerHealthObserver player1LifeObserver;
        private PlayerHealthObserver player2LifeObserver;

        public Board()
        {
            currentRound = 0;
            player1 = new Player();
            player2 = new Player();

            player1LifeObserver = new PlayerHealthObserver();
            player2LifeObserver = new PlayerHealthObserver();

            var player1HandObserver = new PlayerHandObserver();
            var player2HandObserver = new PlayerHandObserver();


            // Subscribe the observers to the players
            player1.HealthObservable.Attach(player1LifeObserver);
            player1.HandObservable.Attach(player1HandObserver);

            player2.HealthObservable.Attach(player2LifeObserver);
            player2.HandObservable.Attach(player2HandObserver);

        }

        public static Board Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Board();
                }
                return instance;
            }
        }

        public void StartGame()
        {
            PrepareGame();
            // Add whatever is needed
        }

        public void PrepareGame()
        {
            // Shuffle decks of each player
            // Players draw cards up until the MaxCardsInHand (7)
            Console.WriteLine(player1.Health);
            Console.WriteLine(player2.Health);
            Console.WriteLine("Press enter to attack player 1 with 5 damage...");
            Console.ReadKey();

            // Hit player 1 with 5 damage
            int damageAmount = 5; // Let's say the damage amount is 10
            player1.Health -= damageAmount;

            Console.WriteLine("Press enter to draw a card for player 1...");
            Console.ReadKey();

            player1.DrawCard();
            Console.WriteLine("Press enter to continue...");
            Console.ReadKey();
        }

        public void StartRound()
        {
            StartTurn(player1);
            StartTurn(player2);

            // End the round and go to the next round
            currentRound++;
        }

        public void StartTurn(Player player)
        {
            // Preparation:
            // Reset temporary effects and reset to original state (example: lands turn back to normal)
            
            // Drawing:
            // Player draws card from deck and add its to their hand

            // Main:
            // Player can play cards
            // Player can attack
            // Opposing player can assign a defender and/or play an instant spell/card

            // Ending:
            // Player must discard cards from their hand until the cards in hand dont exceed MaxCardsInHand (7)
        }

        public void EndTurn()
        {
            // Log what the status of the players at the end of the turn.
            // 
            // Example: 
            // End situation Player1: 4 cards in hand, 3 used lands on the floor, a permanent 
            // creature played in a state of attack, full life.
            // End situation Player2: 6 cards, 1 land on the floor, 5 life.
        }

        public void EndGame()
        {
            // End the game by closing the instance and declaring a winner

            // Game ends by:
            // if player HP <= 0
            // if player Deck.Count <= 0
            // if player forfeits the game

            // Unsubscribe from all observers
            player1.HealthObservable.Detach(player1LifeObserver);
            player2.HealthObservable.Detach(player2LifeObserver);
        }

        public void ClearAllTurnedLandCards(List<Card> playerCards)
        {
            foreach (Card card in playerCards)
            {
                if (card.CardType == Card.Type.Land)
                {
                    LandCard landCard = (LandCard)card;
                    landCard.IsTurned = false;
                }
            }
        }

        public List<LandCard> GetAllTurnedLandCards(List<Card> playerCards)
        {
            List<LandCard> TurnedLandCards = new List<LandCard>();

            foreach (Card card in playerCards)
            {
                if (card.CardType == Card.Type.Land)
                {
                    LandCard landCard = (LandCard)card;
                    TurnedLandCards.Add(landCard);
                }
            }
            return TurnedLandCards;
        }

    }
}
