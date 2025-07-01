using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwinAdventure
{
    public class Player : GameObject, IHaveInventory
    {
        private Bag _inventory;
        private Location _location;

        public Player(string[] ids, string name, string desc) : base(ids, name, desc)
        {
            _inventory = new Bag(new string[] {"inventory", "inv"}, "inventory", "Your personal inventory");
            _location = null;   // Initialize location to null
        }

        public GameObject Locate(string id)
        {
            if (AreYou(id))
            {
                return this;
            }
            GameObject item = _inventory.Locate(id);
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
                return $"You are {Name}, {Description}.\nYou are carrying:\n{_inventory.FullDescription}";
            }
        }

        public Inventory Inventory
        {
            get
            {
                return _inventory.Inventory;
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
