using System;

namespace HRTheGathering.Observers
{
    public class PlayerHealthObserver : IGameObserver<int>
    {
        public void Update(int healthChange)
        {
            Console.WriteLine("Player health changed to: " + healthChange + "\n");
        }
    }
}
