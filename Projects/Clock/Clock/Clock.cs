using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clock
{
    public class Clock
    {
        private Counter _hours;
        private Counter _minutes;
        private Counter _seconds;
        private bool _is24HourFormat;

        public Clock()
        {
            Console.WriteLine("Please enter your student ID: ");
            string? input = Console.ReadLine();
            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentNullException("Student ID cannot be null or empty.");
            }
            int studentID = int.Parse(input);

            int lastDigit = studentID % 10;
            _is24HourFormat = lastDigit > 5;

            _hours = new Counter("hours");
            _minutes = new Counter("minutes");
            _seconds = new Counter("seconds");
        }

        public Clock(bool is24HourFormat)
        {
            this._is24HourFormat = is24HourFormat;
            _hours = new Counter("hours");
            _minutes = new Counter("minutes");
            _seconds = new Counter("seconds");
        }

        public void Tick()
        {
            _seconds.Increment();
            if (_seconds.Ticks >= 60)
            {
                _seconds.Reset();
                _minutes.Increment();
                if (_minutes.Ticks >= 60)
                {
                    _minutes.Reset();
                    _hours.Increment();
                    int maxHours = 24;
                    if (_hours.Ticks >= maxHours)
                    {
                        _hours.Reset();
                    }
                }
            }
        }

        public void Reset()
        {
            _hours.Reset();
            _minutes.Reset();
            _seconds.Reset();
        }

        public string Time
        {
            get
                {
                if (_is24HourFormat)
                {
                    return ($"{_hours.Ticks:D2}:{_minutes.Ticks:D2}:{_seconds.Ticks:D2}");
                }
                else
                {
                    int displayHours = _hours.Ticks % 12;
                    // Convert 0 hours to 12 for 12-hour format
                    if (displayHours == 0) displayHours = 12; 
                    string period = _hours.Ticks < 12 ? "AM" : "PM";
                    return ($"{displayHours:D2}:{_minutes.Ticks:D2}:{_seconds.Ticks:D2} {period}");
                }
            }
        }

        public int Hours
        {
            get
            {
                return _hours.Ticks;
            }
        }

        public int Minutes
        {
            get
            {
                return _minutes.Ticks;
            }
        }

        public int Seconds
        {
            get
            {
                return _seconds.Ticks;
            }
        }
    }
}
