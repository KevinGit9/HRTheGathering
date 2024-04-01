using HRTheGathering.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRTheGathering.Cards
{
    public class LandCard : Card
    {
        public bool IsTurned { get; set; } = false;

        public void TurnCard()
        {
            this.IsTurned = true;
        }
    }
}