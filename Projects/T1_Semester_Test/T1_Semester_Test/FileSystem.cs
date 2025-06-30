using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T1_Semester_Test
{
    public class FileSystem
    {
        private List<Thing> _contents;

        public FileSystem()
        {
            _contents = new List<Thing>();
        }

        public void Add(Thing thing)
        {
            _contents.Add(thing);
        }

        public void PrintContents()
        {
            Console.WriteLine("This File System contains:");
            foreach (var thing in _contents)
            {
                Console.Write(thing.PrintDescription());
            }
        }
    }
}
