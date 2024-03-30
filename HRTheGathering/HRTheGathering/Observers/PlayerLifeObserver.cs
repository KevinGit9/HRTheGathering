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
        private Player _player;

        public PlayerLifeObserver(Player player)
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

        public void OnNext(int healthChange)
        {
            // Example: Update player's health based on the received health change
            _player.HP += healthChange; // Assuming healthChange represents a change in health (e.g., damage or healing)
            Console.WriteLine($"{_player.Name} life updated: {_player.HP}");
        }
    }
}
