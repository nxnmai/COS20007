using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace SwinAdventure
{
    public class Location : GameObject, IHaveInventory
    {
        private Inventory _inventory;

        public Location(string[] ids, string name, string desc) : base(ids, name, desc)
        {
            _inventory = new Inventory();
        }

        public GameObject Locate(string id)
        {
            if (AreYou(id))
            {
                return this;
            }
            return _inventory.Fetch(id);
        }

        public override string FullDescription
        {
            get
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append($"You are in {Name}, {Description}.\n");
                if (_inventory.ItemList.Length > 0)
                {
                    stringBuilder.Append("You can see:\n");
                    stringBuilder.Append($"{_inventory.ItemList}");
                }
                return stringBuilder.ToString();
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
    }
}
