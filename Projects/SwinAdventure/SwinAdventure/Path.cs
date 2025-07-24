using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwinAdventure
{
    public class Path : IdentifiableObject
    {
        private Location _destination;

        public Path(string[] idents, Location destination) : base(idents)
        {
            _destination = destination;
        }

        public void Move(Player player)
        {
            player.Location = _destination;
        }

        public Location Destination
        {
            get
            {
                return _destination;
            }
        }
    }
}
