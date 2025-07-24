using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwinAdventure
{
    public class MoveCommand : Command
    {
        public MoveCommand() : base(new string[] { "move", "go", "head", "leave" })
        { 
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

            string pathID = text[1].ToLower();
            Path path = p.Location.LocatePath(pathID);

            if (path == null)
            {
                return $"No path found for direction: {pathID}.";
            }

            path.Move(p);
            return $"Moved to {p.Location.Name}.";
        }
    }
}
