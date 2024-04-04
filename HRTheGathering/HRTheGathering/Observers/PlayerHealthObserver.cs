using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
