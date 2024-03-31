using HRTheGathering.Cards;
using HRTheGathering.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRTheGathering.Observers
{
    class PlayerLifeObserver : IObserver<int>
    {
        private IDisposable unsubscriber;
        private Player _player;

        public PlayerLifeObserver(Player player)
        {
            _player = player;
        }

        public virtual void Subscribe(IObservable<int> provider)
        {
            unsubscriber = provider.Subscribe(this);
        }

        public virtual void Unsubscribe()
        {
            unsubscriber.Dispose();
        }

        public void OnCompleted()
        {
            Console.WriteLine("Health changed won't be observerd anymore.");
        }

        public void OnError(Exception error)
        {
            // Do nothing
        }

        public void OnNext(int healthChange)
        {
            // Change player HP based on Damage taken or Healing received
            _player.HP += healthChange;
            Console.WriteLine($"{_player.Name} life updated: {_player.HP}");
        }
    }
}
