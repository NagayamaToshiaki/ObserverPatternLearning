using System;
using System.Collections.Generic;

namespace ObserverPatternLearning
{
    class Unsubscriber<T> : IDisposable
    {
        private HashSet<IObserver<T>> _Obserbers;
        private IObserver<T> _Obserber;

        public Unsubscriber(HashSet<IObserver<T>> observers, IObserver<T> observer)
        {
            _Obserbers = observers;
            _Obserber = observer;
        }

        public void Dispose()
        {
            _Obserbers.Remove(_Obserber);
        }
    }
}
