using HRTheGathering.Cards;
using HRTheGathering.Players;

namespace HRTheGathering.Board
{
    class Board
    {
        private static Board instance;
        private int currentTurn;
        private Player player1;
        private Player player2;
        private List<Card> player1Cards;
        private List<Card> player2Cards;

        private Board()
        {
            currentTurn = 0;
            player1Cards = new List<Card>();
            player2Cards = new List<Card>();
            player1 = new Player();
            player2 = new Player();
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

        public void AddCard(Card card, List<Card> playerCards)
        {
            playerCards.Add(card);
        }

        public void RemoveCard(Card card, List<Card> playerCards)
        {
            playerCards.Remove(card);
        }

        public void EndGame()
        {
            // End the game by closing the instance and declaring a winner
            
            // Game ends by:
            // if player HP == 0
            // if player Deck == 0
            // if player forfeits the game
        }

    }
}
