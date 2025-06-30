// Student Name: Nguyen Xuan Nang Mai. Student ID: 105549980.
using System.Diagnostics.Metrics;

namespace Null
{
    internal class Program
    {
        private static void PrintCounters(Counter[] counters)
        {
            foreach (Counter c in counters)
            {
                Console.WriteLine("{0} is {1}", c.Name, c.Ticks);
            }
        }

        static void Main(string[] args)
        {
            Counter[] myCounters = new Counter[3];

            myCounters[0] = new Counter("Counter 1");
            myCounters[1] = new Counter("Counter 2");
            myCounters[2] = myCounters[0];

            for (int i = 1; i < 10; i++)
            {
                myCounters[0].Increment();
            }
            for (int i = 1; i < 15; i++)
            {
                myCounters[1].Increment();
            }

            PrintCounters(myCounters);
            myCounters[2].Reset();
            PrintCounters(myCounters);

            // Question 9: Set myCounters to null and try to change myCounters[0]
            Console.WriteLine("\nSetting myCounters to null");
            myCounters = null;

            try
            {
                Console.WriteLine("Attempting to change myCounters[0]");
                myCounters[0] = new Counter("New Counter");
                PrintCounters(myCounters);
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            //myCounters[0].ResetByDefault();
            // The code cannot run because the count value exceeds the maximum integer value.
        }
    }
}