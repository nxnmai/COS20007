using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T1_Semester_Test
{
    public class File : Thing
    {
        private int _size;
        private string _extension;

        public File(string name, int size, string extension) : base(name)
        {
            this._size = size;
            this._extension = extension;
        }

        public override int GetSize()
        {
            return _size;
        }

        public override string PrintDescription()
        {
            return $"File '{name}.{_extension}' Size: {_size} bytes\n";
        }
    }
}
