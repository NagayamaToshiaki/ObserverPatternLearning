using System;
using System.Collections;
using System.Collections.Generic;

namespace ObserverPatternLearning
{
    /// <summary>
    /// Manages Baggage.
    /// </summary>
    internal class BaggageHandlerSubject : IObservable<IBaggageInfo>, IEnumerable<IBaggageInfo>
    {
        /// <summary>
        /// The list of observers.
        /// </summary>
        private HashSet<IObserver<IBaggageInfo>> _Observers = new HashSet<IObserver<IBaggageInfo>>();

        /// <summary>
        /// The list of Baggage informations.
        /// </summary>
        private HashSet<IBaggageInfo> _Flights = new HashSet<IBaggageInfo>();
        
        /// <summary>
        /// Add observer to Subscriver List.
        /// </summary>
        /// <param name="observer">An observer to add.</param>
        /// <returns>A reference to an interface that allows observers to stop receiving notifications before the provider has finished sending them.</returns>
        public IDisposable Subscribe(IObserver<IBaggageInfo> observer)
        {
            if (_Observers.Contains(observer))
            {
                return new Unsubscriber<IBaggageInfo>(_Observers, observer);
            }
            
            _Observers.Add(observer);
            foreach (IBaggageInfo flight in _Flights)
            {
                observer.OnNext(flight);
            }

            return new Unsubscriber<IBaggageInfo>(_Observers, observer);
        }

        /// <summary>
        /// Change this handler's baggage status with info.
        /// </summary>
        /// <param name="info">Info to change this handler's status.</param>
        public void ChangeBaggageStatus(IBaggageInfo info)
        {
            info.ChangeBaggageStatus(this);
        }
        
        /// <summary>
        /// Notify a baggage info to the observers.
        /// </summary>
        /// <param name="info">Baggage info to notify.</param>
        public void OnNext(IBaggageInfo info)
        {
            foreach (IObserver<IBaggageInfo> observer in _Observers)
            {
                observer.OnNext(info);
            }
        }

        /// <summary>
        /// Call this when the last baggage is arrived.
        /// </summary>
        public void LastBaggageClaimed()
        {
            foreach (IObserver<IBaggageInfo> observer in _Observers)
            {
                observer.OnCompleted();
            }

            _Observers.Clear();
        }

        /// <summary>
        /// Add a baggage info to this instance.
        /// </summary>
        /// <param name="item">Baggage info to add.</param>
        public void Add(IBaggageInfo item)
        {
            _Flights.Add(item);
            OnNext(item);
        }
        
        /// <summary>
        /// Remove a baggage info from this instance.
        /// </summary>
        /// <param name="item">Baggage info to remove.</param>
        /// <returns>Whether the item is in the instance.</returns>
        public bool Remove(IBaggageInfo item)
        {
            var result = _Flights.Remove(item);
            return result;
        }

        /// <summary>
        /// Get the enumlator for the list.
        /// </summary>
        /// <returns>The enumlator for the list.</returns>
        public IEnumerator<IBaggageInfo> GetEnumerator() => _Flights.GetEnumerator();

        /// <summary>
        /// Generic version. Just for the sake of backward compatibility.
        /// </summary>
        /// <returns>The enumlator for the list.</returns>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
