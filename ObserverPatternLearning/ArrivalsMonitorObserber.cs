using System;
using System.Collections;
using System.Collections.Generic;

namespace ObserverPatternLearning
{
    /// <summary>
    /// The display of the arrivals.
    /// </summary>
    class ArrivalsMonitorObserber : IObserver<IBaggageInfo>, IEnumerable<IBaggageInfo>
    {
        /// <summary>
        /// The name of the monitor.
        /// </summary>
        private readonly string _Name;

        /// <summary>
        /// The baggage infomations of the flight.
        /// </summary>
        private SortedSet<IBaggageInfo> _FlightInfos = new SortedSet<IBaggageInfo>(Comparer<IBaggageInfo>.Default);

        /// <summary>
        /// Dispose this when unsubscribing.
        /// </summary>
        private IDisposable _Cancellation;

        /// <summary>
        /// Instanciate the monitor.
        /// </summary>
        /// <param name="name">The name of the monitor.</param>
        public ArrivalsMonitorObserber(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("The observer must be assigned a name.");
            }

            _Name = name;
        }

        /// <summary>
        /// Subscribe to the observer.
        /// </summary>
        /// <param name="provider">The observer to subscribe.</param>
        public virtual void Subscribe(BaggageHandlerSubject provider)
        {
            _Cancellation = provider.Subscribe(this);
        }

        /// <summary>
        /// Unsubscribe the observer.
        /// </summary>
        public virtual void Unsubscribe()
        {
            _Cancellation.Dispose();
            _FlightInfos.Clear();
        }

        /// <summary>
        /// Notifies the observer that the provider has finished sending push-based notifications.
        /// </summary>
        public virtual void OnCompleted()
        {
            _FlightInfos.Clear();
        }

        /// <summary>
        /// No implementation needed: Method is not called by the BaggageHandler class.
        /// </summary>
        /// <param name="e">An exeption.</param>
        public virtual void OnError(Exception e)
        {
            // No implementation.
        }

        /// <summary>
        /// Provides the observer with new data.
        /// </summary>
        /// <param name="info">The current notification information.</param>
        public virtual void OnNext(IBaggageInfo info)
        {
            bool updated = info.OnNext(this);

            if (updated)
            {
                Display();
            }
        }

        /// <summary>
        /// Displays the informations this monitor has.
        /// </summary>
        private void Display()
        {
            Console.WriteLine($"Arrivals information from {_Name}");
            foreach (IBaggageInfo flightInfo in _FlightInfos)
            {
                Console.WriteLine(flightInfo);
            }

            Console.WriteLine();
        }

        /// <summary>
        /// Add baggage info to this monitor.
        /// </summary>
        /// <param name="item">The baggage info to add.</param>
        public void Add(IBaggageInfo item) => _FlightInfos.Add(item);
        
        /// <summary>
        /// Check if this monitor has the baggage info.
        /// </summary>
        /// <param name="item">The baggage info to search.</param>
        /// <returns>Whether this monitor has the info.</returns>
        public bool Contains(IBaggageInfo item) => _FlightInfos.Contains(item);
        
        /// <summary>
        /// Remove the baggage info from this monitor.
        /// </summary>
        /// <param name="item">The baggage info to remove.</param>
        /// <returns>Whether this monitor removed the info.</returns>
        public bool Remove(IBaggageInfo item) => _FlightInfos.Remove(item);
        
        /// <summary>
        /// Get the enumlator for the list.
        /// </summary>
        /// <returns>The enumlator for the list.</returns>
        public IEnumerator<IBaggageInfo> GetEnumerator() => _FlightInfos.GetEnumerator();

        /// <summary>
        /// Generic version. Just for the sake of backward compatibility.
        /// </summary>
        /// <returns>The enumlator for the list.</returns>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
