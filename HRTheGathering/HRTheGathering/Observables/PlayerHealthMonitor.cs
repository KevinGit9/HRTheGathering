using HRTheGathering.Players;

namespace HRTheGathering.Observables
{
    public class PlayerHealthMonitor : IObservable<Player>
    {
        List<IObserver<Player>> observers;

        public PlayerHealthMonitor()
        {
            observers = new List<IObserver<Player>>();
        }

        private class Unsubscriber : IDisposable
        {
            private List<IObserver<Player>> _observers;
            private IObserver<Player> _observer;

            public Unsubscriber(List<IObserver<Player>> observers, IObserver<Player> observer)
            {
                this._observers = observers;
                this._observer = observer;
            }

            public void Dispose()
            {
                if (!(_observer == null)) _observers.Remove(_observer);
            }
        }

        public IDisposable Subscribe(IObserver<Player> observer)
        {
            if (!observers.Contains(observer))
                observers.Add(observer);

            return new Unsubscriber(observers, observer);
        }
    }
}
