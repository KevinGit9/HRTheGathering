using HRTheGathering.Cards;
using HRTheGathering.Observables;
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
        public PlayerLifeObserver player1LifeObserver;
        public PlayerLifeObserver player2LifeObserver;

        public Board()
        {
            currentRound = 0;
            player1 = new Player();
            player2 = new Player();

            var provider = new PlayerHealthMonitor();

            player1LifeObserver = new PlayerLifeObserver(player1);
            player2LifeObserver = new PlayerLifeObserver(player2);

            // Subscribe the observers to the players
            player1LifeObserver.Subscribe(provider);
            player2LifeObserver.Subscribe(provider);
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


            /*
            // Simulate damage
            //
            */
            Console.WriteLine($"player 1 HP: {player1.HP}");
            Console.WriteLine($"player 2 HP: {player2.HP}");
            Console.WriteLine("Press enter to attack player 1 with 5 damage...");
            Console.ReadKey();

            // Hit player 1 with 5 damage
            int damageAmount = 5;
            player1.HP -= damageAmount;
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
            player1LifeObserver.Unsubscribe();
            player2LifeObserver.Unsubscribe();
        }

    }
}
