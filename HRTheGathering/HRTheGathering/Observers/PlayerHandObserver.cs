using HRTheGathering.Cards;
using HRTheGathering.Players;

namespace HRTheGathering.Observers
{
    class PlayerHandObserver : IObserver<List<Card>>
    {
        private Player _player;

        public PlayerHandObserver(Player player)
        {
            _player = player;
        }

        public void OnCompleted()
        {
            // Optionally implement logic for completion of observation
        }

        public void OnError(Exception error)
        {
            // Optionally handle errors
        }

        public void OnNext(List<Card> card)
        {
     
        }
    }
}
