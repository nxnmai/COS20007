using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwinAdventure
{
    public class LookCommand : Command
    {
        public LookCommand() : base(new string[] { "look" })
        {
        }
        
        public override string Execute(Player p, string[] text)
        {
            if (text.Length == 1 && text[0].ToLower() == "look")
            {
                if (p.Location != null)
                {
                    return p.Location.FullDescription;
                }
                return "You are not in any location.";
            }
            
            if (text.Length != 3 && text.Length != 5)
            {
                return "I don't know how to look like that";
            }

            if (text[0] != "look")
            {
                return "Error in look input";
            }

            if (text[1] != "at")
            {
                return "What do you want to look at?";
            }

            IHaveInventory container;
            if (text.Length == 3)
            {
                container = p;
            }
            else if (text.Length == 5)
            {
                if (text[3] != "in")
                {
                    return "What do you want to look in?";
                }
                container = FetchContainer(p, text[4]) as IHaveInventory;
                if (container == null)
                {
                    return $"I cannot find the <{text[4]}>";
                }
            }
            else
            {
                return "I don't know how to look like that";
            }

            return LookAtIn(container, text[2]);
        }

        private IHaveInventory FetchContainer(Player p, string containerId)
        {
            return p.Locate(containerId) as IHaveInventory;
        }

        private string LookAtIn(IHaveInventory container, string thingId)
        {
            GameObject obj = container.Locate(thingId);
            if (obj == null)
            {
                return $"I cannot find the <{thingId}> in the {container.Name}";
            }

            return obj.FullDescription;
        }
    }
}
