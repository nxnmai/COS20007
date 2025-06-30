// Student Name: Nguyen Xuan Nang Mai. Student ID: 105549980.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clock
{
    public class Counter
    {
        private int _count;
        private string _name;

        public Counter(string name)
        {
            _name = name;
            _count = 0;
        }

        public void Increment()
        {
            _count++;
        }

        public void Reset()
        {
            _count = 0;
        }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }

        }

        public int Ticks
        {
            get
            {
                return _count;
            }
        }

        public void ResetByDefault()
        {
            try
            {
                checked
                {
                    _count = 0;
                }
            }
            catch (OverflowException ex)
            {
                Console.WriteLine($"Overflow error: {ex.Message}");
            }
        }
    }
}
