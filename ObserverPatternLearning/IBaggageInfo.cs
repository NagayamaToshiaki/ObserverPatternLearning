using System;

namespace ObserverPatternLearning
{
    interface IBaggageInfo : IEquatable<IBaggageInfo>, IComparable<IBaggageInfo>
    {
        /// <summary>
        /// The flight Number.
        /// </summary>
        uint FlightNumber { get; }
        
        /// <summary>
        /// Describe the process of IObserver.OnNext().
        /// </summary>
        /// <param name="observer">the observer of IBaggageInfo.</param>
        /// <returns>Whether observer has this info.</returns>
        bool OnNext(ArrivalsMonitorObserber observer);

        /// <summary>
        /// Describe the process of BaggageHandler.ChangeBaggageStatus().
        /// </summary>
        /// <param name="handler">The handler of IBaggageInfo.</param>
        void ChangeBaggageStatus(BaggageHandlerSubject handler);
    }
}