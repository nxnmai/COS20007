using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace SwinAdventure
{
    public class MoveCommand : Command
    {
        private Dictionary<string, string> _directionSynonyms;

        public MoveCommand() : base(new string[] { "move", "go", "head", "leave" })
        {
            _directionSynonyms = new Dictionary<string, string>
            {
                {"n", "north" }, {"s", "south"}, {"e", "east"}, {"w", "west"},
                {"ne", "northeast"}, {"nw", "northwest"}, {"se", "southeast"}, {"sw", "southwest"},
                {"u", "up" }, {"d", "down" }
            };
        }   

        public override string Execute(Player p, string[] text)
        {
            if (text.Length != 2)
            {
                return "I don't know how to move like that.";
            }

            if (!AreYou(text[0]))
            {
                return "Error in move input.";
            }

            string direction = text[1].ToLower();
            if (_directionSynonyms.ContainsKey(direction))
            {
                direction = _directionSynonyms[direction];
            }

            Path path = p.Location.LocatePath(direction);

            if (path == null)
            {
                return $"No path found for direction: {direction}.";
            }

            path.Move(p);
            return $"Moved to {p.Location.Name}.";
        }
    }
}
