using System;
using System.Diagnostics;

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

            //Get the current process
            System.Diagnostics.Process proc = System.Diagnostics.Process.GetCurrentProcess();
            Console.WriteLine("Current process: {0}", proc.ToString());

            //Display the total physical memory size allocated for the current process
            Console.WriteLine("Physical memory usage: {0} bytes", proc.WorkingSet64);

            // Display peak memory statistics for the process
            Console.WriteLine("Peak physical memory usage {0} bytes", proc.PeakWorkingSet64);
        }
    }
}

