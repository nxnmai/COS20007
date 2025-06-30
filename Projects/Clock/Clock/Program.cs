using System;

namespace Clock
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Creating a new clock instance");
            Clock clock = new Clock();

            Console.WriteLine("Initial time: " + clock.Time);

            Console.WriteLine("Ticking the clock 5 times");
            for (int i = 0; i < 5; i++)
            {
                clock.Tick();
                Console.WriteLine($"After tick {i + 1}: {clock.Time}");
            }

            Console.WriteLine("Resetting the clock");
            clock.Reset();
            Console.WriteLine("After reset: " + clock.Time);
        }
    }
}
