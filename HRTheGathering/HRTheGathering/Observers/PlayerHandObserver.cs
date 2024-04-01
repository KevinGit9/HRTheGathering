using HRTheGathering.Cards;
using HRTheGathering.Players;

namespace HRTheGathering.Observers
{
    class PlayerHandObserver : IGameObserver<List<Card>>
    {
        public void Update(List<Card> newHand)
        {
            Console.WriteLine("Player hand changed:");
            foreach (var card in newHand)
            {
                Console.WriteLine(card.Name);
            }
        }
    }
}
