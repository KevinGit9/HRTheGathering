using System;

namespace HRTheGathering.Observers
{
    public interface IGameObserver<T>
    {
        void Update(T value);
    }
}
