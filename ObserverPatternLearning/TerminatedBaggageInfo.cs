using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObserverPatternLearning
{
    /// <summary>
    /// Describes the information of the baggage that ended operation.
    /// </summary>
    class TerminatedBaggageInfo : IEquatable<IBaggageInfo>, IBaggageInfo
    {
        /// <summary>
        /// The flight Number.
        /// </summary>
        public uint FlightNumber { get; }
        
        /// <summary>
        /// Copy baggage data into this class.
        /// </summary>
        /// <param name="info">The info to copy.</param>
        public TerminatedBaggageInfo(uint flightNumber)
        {
            FlightNumber = flightNumber;
        }

        /// <summary>
        /// The implementation of <see cref="IEquatable{IBaggageInfo}.Equals()"/>.
        /// </summary>
        /// <param name="other">The Info to compare.</param>
        /// <returns>Whether this and <paramref name="other"/> is same.</returns>
        public bool Equals(IBaggageInfo other)
        {
            return FlightNumber == other.FlightNumber;
        }

        /// <summary>
        /// The implementation of object.Equals().
        /// </summary>
        /// <param name="other">The Info to compare.</param>
        /// <returns>Whether this and <paramref name="other"/> is same.</returns>
        public override bool Equals(object obj)
        {
            if (!(obj is IBaggageInfo))
            {
                return false;
            }
            return Equals((BaggageInfo)obj);
        }

        /// <summary>
        /// Compare this instance to other IBaggageInfo object.
        /// </summary>
        /// <param name="other">The IBaggageInfo object to compare.</param>
        /// <returns>A value that indicates the relative order of the objects being compared.</returns>
        public int CompareTo(IBaggageInfo other)
        {
            return (int)FlightNumber - (int)other.FlightNumber;
        }

        /// <summary>
        /// Get the hash code of the object.
        /// </summary>
        /// <returns>the hash code.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(FlightNumber);
        }

        /// <summary>
        /// Remove this info from the observer if it has this object.
        /// </summary>
        /// <param name="observer">The observer to add this info.</param>
        /// <returns>Whether this element is added.</returns>
        public bool OnNext(ArrivalsMonitorObserber observer)
        {
            var flightsToRemove = observer.Where(ob => ob.FlightNumber == FlightNumber).ToList();

            if (!flightsToRemove.Any())
            {
                return false;
            }

            foreach (IBaggageInfo flightToRemove in flightsToRemove)
            {
                observer.Remove(flightToRemove);
            }

            flightsToRemove.Clear();

            return true;
        }

        /// <summary>
        /// Converts this info to string.
        /// </summary>
        /// <returns>The stringification of this object.</returns>
        public override string ToString()
        {
            return $"The {FlightNumber,5} flight is ended.";
        }

        /// <summary>
        /// Try to remove this info from the handler.
        /// </summary>
        /// <param name="handler">The handler to remove the info.</param>
        public void ChangeBaggageStatus(BaggageHandlerSubject handler)
        {
            var flightsToRemove = handler.Where(fl => fl.FlightNumber == FlightNumber).ToList();
            if (flightsToRemove.Any())
            {
                handler.OnNext(this);
            }
            
            foreach (IBaggageInfo info in flightsToRemove)
            {
                handler.Remove(info);
            }
        }
    }
}
