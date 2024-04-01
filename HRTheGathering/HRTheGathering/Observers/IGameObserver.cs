using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRTheGathering.Observers
{
    public interface IGameObserver<T>
    {
        void Update(T value);
    }
}
