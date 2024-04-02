using System.Collections.Generic;

namespace HRTheGathering.Observers
{
    public class Observable<T>
    {
        private List<IGameObserver<T>> observers = new List<IGameObserver<T>>();

        public void Attach(IGameObserver<T> observer)
        {
            observers.Add(observer);
        }

        public void Detach(IGameObserver<T> observer)
        {
            observers.Remove(observer);
        }

        public void NotifyObservers(T data)
        {
            foreach (var observer in observers)
            {
                observer.Update(data);
            }
        }
    }
}
