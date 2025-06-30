using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Clock.Test
{
    [TestFixture]
    public class ClockTest
    {
        private Clock _clock;

        [SetUp]
        public void Setup()
        {
            _clock = new Clock(true); // Initialize with 24-hour format
        }

        [Test]
        public void Clock_24Hour_Format()
        {
            Assert.That(_clock.Hours, Is.EqualTo(0), "Hours should start at 0.");
            Assert.That(_clock.Minutes, Is.EqualTo(0), "Minutes should start at 0.");
            Assert.That(_clock.Seconds, Is.EqualTo(0), "Seconds should start at 0.");
            Assert.That(_clock.Time, Is.EqualTo("00:00:00"), "Time should start at 00:00:00.");
        }

        [Test]
        public void Clock_12Hour_Format()
        {
            _clock = new Clock(false); // Set to 12-hour format

            Assert.That(_clock.Hours, Is.EqualTo(0), "Hours should start at 0 internally in 12-hour format.");
            Assert.That(_clock.Time, Is.EqualTo("12:00:00 AM"), "Time should start at 12:00:00 AM.");
        }

        [Test]
        public void Seconds_Rollover_24Hour_Format()
        {
            _clock.Reset();

            for (int i = 0; i < 60; i++)
            {
                _clock.Tick();
            }

            Assert.That(_clock.Seconds, Is.EqualTo(0), "Seconds should roll over to 0 after 60 ticks.");
            Assert.That(_clock.Minutes, Is.EqualTo(1), "Minutes should increment to 1.");
            Assert.That(_clock.Time, Is.EqualTo("00:01:00"), "Time should be 00:01:00 after 60 seconds.");
        }

        [Test]
        public void Hours_Rollover_24Hour_Format()
        {
            _clock.Reset();
            for (int i = 0; i < 23 * 3600 + 59 * 60 + 59; i++)
            {
                _clock.Tick();
            }
            _clock.Tick();

            Assert.That(_clock.Hours, Is.EqualTo(0), "Hours should roll over to 0 after 24.");
            Assert.That(_clock.Time, Is.EqualTo("00:00:00"), "Time should be 00:00:00 after 24 hours.");
        }

        [Test]
        public void Hours_Rollover_12Hour_Format()
        {
            _clock = new Clock(false); // Set to 12-hour format
            _clock.Reset();
            for (int i = 0; i < 11 * 3600 + 59 * 60 + 59; i++)
            {
                _clock.Tick();
            }
            _clock.Tick();

            Assert.That(_clock.Hours, Is.EqualTo(12), "Hours should be 12 internally.");
            Assert.That(_clock.Time, Is.EqualTo("12:00:00 PM"), "Time should be 12:00:00 PM after 12 hours in 12-hour format.");
        }

        [Test]
        public void AM_PM_Transition_12Hour_Format()
        {
            _clock = new Clock(false); // Set to 12-hour format
            _clock.Reset();
            for (int i = 0; i < 13 * 3600; i++)
            {
                _clock.Tick();
            }

            Assert.That(_clock.Time, Is.EqualTo("01:00:00 PM"), "Time should be 01:00:00 PM after 13 hours.");
        }

        [Test]
        public void Reset_12Hour_Format()
        {
            _clock = new Clock(false); // Set to 12-hour format
            _clock.Tick();
            _clock.Reset();

            Assert.That(_clock.Hours, Is.EqualTo(0), "Hours should be 0 internally.");
            Assert.That(_clock.Time, Is.EqualTo("12:00:00 AM"), "Time should be 12:00:00 AM in 12-hour format.");
        }

        [Test]
        public void Reset_24Hour_Format()
        {
            _clock.Tick();
            _clock.Reset();

            Assert.That(_clock.Hours, Is.EqualTo(0), "Hours should be 0.");
            Assert.That(_clock.Time, Is.EqualTo("00:00:00"), "Time should be 00:00:00 in 24-hour format.");
        }
    }
}
