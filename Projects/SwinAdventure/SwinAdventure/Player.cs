using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwinAdventure
{
    public class Player : GameObject, IHaveInventory
    {
        private Inventory _inventory;
        private Location _location;

        public Player(string[] ids, string name, string desc) : base(new string[] { "me", "inventory" }, name, desc)
        {
            _inventory = new Inventory();
            _location = null;   // Initialize location to null
        }

        public GameObject Locate(string id)
        {
            if (AreYou(id))
            {
                return this;
            }
            GameObject item = _inventory.Fetch(id);
            if (item != null)
            {
                return item;
            }
            if (_location != null)
            {
                return _location.Locate(id);
            }
            return null;
        }

        public override string FullDescription
        {
            get
            {
                StringBuilder desc = new StringBuilder();
                desc.Append($"You are {Name}, {Description}.\nYou are carrying:\n{_inventory.ItemList}");
                if (_location != null)
                {
                    desc.Append($"You are in {_location.Name}: {_location.FullDescription}");
                    if (_location.LocatePath("north") != null || _location.LocatePath("south") != null ||
                        _location.LocatePath("east") != null || _location.LocatePath("west") != null)
                    {
                        desc.Append("\nAvailable paths:");
                        if (_location.LocatePath("north") != null)
                        {
                            desc.Append("\n\tnorth");
                        }
                        if (_location.LocatePath("south") != null)
                        {
                            desc.Append("\n\tsouth");
                        }
                        if (_location.LocatePath("east") != null)
                        {
                            desc.Append("\n\teast");
                        }
                        if (_location.LocatePath("west") != null)
                        {
                            desc.Append("\n\twest");
                        }
                    }
                }
                return desc.ToString();
            }
        }

        public Inventory Inventory
        {
            get
            {
                return _inventory;
            }
        }

        public string Name
        {
            get
            {
                return base.Name;
            }
        }

        public Location Location
        {
            get
            {
                return _location;
            }
            set
            {
                _location = value;
            }
        }
    }
}
