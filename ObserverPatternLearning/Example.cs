using System;

namespace ObserverPatternLearning
{
    class Example
    {
        static void Main(string[] args)
        {
            BaggageHandlerSubject provider = new BaggageHandlerSubject();
            ArrivalsMonitorObserber observer1 = new ArrivalsMonitorObserber("BaggageClaimMonitor1");
            ArrivalsMonitorObserber observer2 = new ArrivalsMonitorObserber("SecurityExit");

            IBaggageInfo detroit = new BaggageInfo("Detroit", 712, 3);
            provider.ChangeBaggageStatus(detroit);

            observer1.Subscribe(provider);

            IBaggageInfo kalamazoo = new BaggageInfo("Kalamazoo", 712, 3);
            provider.ChangeBaggageStatus(kalamazoo);

            IBaggageInfo newYorkKennedy = new BaggageInfo("New York-Kennedy", 400, 1);
            provider.ChangeBaggageStatus(newYorkKennedy);

            IBaggageInfo anotherDetroit = new BaggageInfo("Detroit", 712, 3);
            provider.ChangeBaggageStatus(anotherDetroit);

            observer2.Subscribe(provider);

            IBaggageInfo sanFrancisco = new BaggageInfo("San Francisco", 511, 2);
            provider.ChangeBaggageStatus(sanFrancisco);

            IBaggageInfo sevenTwelve = new TerminatedBaggageInfo(detroit.FlightNumber);
            provider.ChangeBaggageStatus(sevenTwelve);

            observer2.Unsubscribe();

            IBaggageInfo fourHundred = new TerminatedBaggageInfo(newYorkKennedy.FlightNumber);
            provider.ChangeBaggageStatus(fourHundred);

            provider.LastBaggageClaimed();

            Console.ReadKey();
        }
    }
}
