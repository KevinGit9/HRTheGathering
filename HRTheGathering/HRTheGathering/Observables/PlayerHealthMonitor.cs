using HRTheGathering.Players;

namespace HRTheGathering.Observables
{
    public class PlayerHealthMonitor : IObservable<int>
    {
        private List<IObserver<int>> observers;

        public PlayerHealthMonitor()
        {
            observers = new List<IObserver<int>>();
        }

        public IDisposable Subscribe(IObserver<int> observer)
        {
            if (!observers.Contains(observer))
                observers.Add(observer);
                        Console.WriteLine($"Observer subscribed: {observer.GetType().Name}");

            return new Unsubscriber(observers, observer);
        }

        private class Unsubscriber : IDisposable
        {
            private List<IObserver<int>> _observers;
            private IObserver<int> _observer;

            public Unsubscriber(List<IObserver<int>> observers, IObserver<int> observer)
            {
                this._observers = observers;
                this._observer = observer;
            }

            public void Dispose()
            {
                if (!(_observer == null)) _observers.Remove(_observer);
            }
        }
    }
}
