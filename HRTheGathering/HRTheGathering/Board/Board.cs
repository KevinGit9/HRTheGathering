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
        private CardFactory cardFactory;

        private Board()
        {
            currentTurn = 0;         
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
