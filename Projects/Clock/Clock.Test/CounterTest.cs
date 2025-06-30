using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Clock.Test
{
    [TestFixture]
    public class CounterTest
    {
        [Test]
        public void Counter_Initialized_At_0()
        {
            Counter counter = new Counter("testCounter");
            int initialCount = counter.Ticks;
            Assert.That(initialCount, Is.EqualTo(0), "The counter should start at 0 when initialized.");
        }

        [Test]
        public void Increment_Called_Once()
        {
            Counter counter = new Counter("testCounter");
            counter.Increment();
            int countAfterIncrement = counter.Ticks;
            Assert.That(countAfterIncrement, Is.EqualTo(1), "The counter should increased by 1 after one increment.");
        }

        [Test]
        public void Increment_Multiple_Times()
        {
            Counter counter = new Counter("testCounter");
            int numberOfIncrements = 10;

            for (int i = 0; i < numberOfIncrements; i++)
            {
                counter.Increment();
            }
            int countAfterIncrements = counter.Ticks;
            
            Assert.That(countAfterIncrements, Is.EqualTo(numberOfIncrements), "The counter should increase by the number of increments.");
        }

        [Test]
        public void Reset_Count_To_0()
        {
            Counter counter = new Counter("testCounter");
            counter.Increment();
            counter.Reset();
            int countAfterReset = counter.Ticks;
            
            Assert.That(countAfterReset, Is.EqualTo(0), "The counter should be reset to 0 after reset.");
        }
    }
}
