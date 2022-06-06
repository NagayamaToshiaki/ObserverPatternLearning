using System;

namespace ObserverPatternLearning
{
    /// <summary>
    /// Describes the information of the baggage.
    /// </summary>
    class BaggageInfo : IBaggageInfo, IComparable<IBaggageInfo>, IEquatable<IBaggageInfo>
    {
        /// <summary>
        /// The flight Number.
        /// </summary>
        public uint FlightNumber { get; }

        /// <summary>
        /// Where the flight is from.
        /// </summary>
        public string From { get; }

        /// <summary>
        /// The number of the baggage.
        /// </summary>
        public uint Carousel { get; }

        /// <summary>
        /// Instanciate the information of the baggage.
        /// </summary>
        /// <param name="from">Where the flight is from.</param>
        /// <param name="flightNumber">The flight Number.</param>
        /// <param name="carousel">The number of the baggage.</param>
        public BaggageInfo(string from, uint flightNumber, uint carousel)
        {
            From = from;
            FlightNumber = flightNumber;
            Carousel = carousel;
        }

        /// <summary>
        /// The implementation of <see cref="IEquatable{IBaggageInfo}.Equals()"/>.
        /// </summary>
        /// <param name="other">The Info to compare.</param>
        /// <returns>Whether this and <paramref name="other"/> is same.</returns>
        public bool Equals(IBaggageInfo other)
        {
            if (other is BaggageInfo)
            {
                return Equals((BaggageInfo)other);
            }
            return FlightNumber == other.FlightNumber;
        }

        /// <summary>
        /// The implementation of <see cref="IEquatable{BaggageInfo}.Equals()"/>.
        /// </summary>
        /// <param name="other">The Info to compare.</param>
        /// <returns>Whether this and <paramref name="other"/> is same.</returns>
        public bool Equals(BaggageInfo other)
        {
            return FlightNumber == other.FlightNumber &&
                From == other.From &&
                Carousel == other.Carousel;
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
            if (obj is BaggageInfo)
            {
                return Equals((BaggageInfo)obj);
            }
            return Equals((IBaggageInfo)obj);
        }

        /// <summary>
        /// Get the hash code of the object.
        /// </summary>
        /// <returns>the hash code.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(FlightNumber, From, Carousel);
        }

        /// <summary>
        /// Add this info to the observer if it doesn't have this object.
        /// </summary>
        /// <param name="observer">The observer to add this info.</param>
        /// <returns>Whether this element is added.</returns>
        public bool OnNext(ArrivalsMonitorObserber observer)
        {
            if (observer.Contains(this))
            {
                return false;
            }

            observer.Add(this);
            return true;
        }

        /// <summary>
        /// Converts this info to string.
        /// </summary>
        /// <returns>The stringification of this object.</returns>
        public override string ToString()
        {
            return $"{From,-20} {FlightNumber,5}  {Carousel, 3}";
        }

        /// <summary>
        /// Compares this instance to other instance.
        /// </summary>
        /// <param name="other">The instance to compare to this.</param>
        /// <returns>A value that indicates the relative order of the objects being compared.</returns>
        public int CompareTo(BaggageInfo other)
        {
            return string.Compare(From, other.From);
        }

        /// <summary>
        /// Compares this instance to other instance.
        /// </summary>
        /// <param name="other">The instance to compare to this.</param>
        /// <returns>A value that indicates the relative order of the objects being compared.</returns>
        public int CompareTo(IBaggageInfo other)
        {
            if (other is BaggageInfo)
            {
                return CompareTo((BaggageInfo)other);
            }
            return (int)FlightNumber - (int)other.FlightNumber;
        }

        /// <summary>
        /// Try to add this info to the handler.
        /// </summary>
        /// <param name="handler">The handler to remove the info.</param>
        public void ChangeBaggageStatus(BaggageHandlerSubject handler)
        {
            handler.Add(this);
        }
    }
}
