using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T1_Semester_Test
{
    public class Folder : Thing
    {
        private List<Thing> _contents;

        public Folder(string name) : base(name)
        {
            _contents = new List<Thing>();
        }

        public void Add(Thing thing)
        {
            _contents.Add(thing);
        }

        public override int GetSize()
        {
            int totalSize = 0;
            foreach (var thing in _contents)
            {
                totalSize += thing.GetSize();
            }
            return totalSize;
        }

        public override string PrintDescription()
        {
            string result = $"The Folder: '{name}' contains {_contents.Count} items totalling {GetSize()} bytes:\n";
            foreach (var thing in _contents)
            {
                result += thing.PrintDescription();
            }
            return result;
        }
    }
}
