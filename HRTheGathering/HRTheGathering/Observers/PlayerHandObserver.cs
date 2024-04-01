using HRTheGathering.Cards;
using HRTheGathering.Players;

namespace HRTheGathering.Observers
{
    class PlayerHandObserver : IGameObserver<List<Card>>
    {
        private Player _player;

        public PlayerHandObserver(Player player)
        {
            _player = player;
        }

        public void Update(List<Card> card)
        {
     
        }
    }
}
