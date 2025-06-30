// Student Name: Nguyen Xuan Nang Mai. Student ID: 105549980.
namespace CounterTask
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

            myCounters[0].ResetByDefault();
            // The code cannot run because the count value exceeds the maximum integer value.
        }
    }
}