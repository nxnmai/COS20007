using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwinAdventure
{
    public class Inventory
    {
        private List<GameObject> _items;

        public Inventory()
        {
            _items = new List<GameObject>();
        }

        public bool HasItem(string id)
        {
            return _items.Exists(item => item.AreYou(id));
        }

        public void Put(GameObject item)
        {
            _items.Add(item);
        }

        public GameObject Take(string id)
        {
            GameObject item = Fetch(id);
            if (item != null)
            {
                _items.Remove(item);
            }
            return item;
        }

        public GameObject Fetch(string id)
        {
            return _items.Find(item => item.AreYou(id));
        }

        public string ItemList
        {
            get
            {
                StringBuilder result = new StringBuilder();
                foreach (GameObject item in _items)
                {
                    result.Append($"\t{item.ShortDescription}\n");
                }
                return result.ToString();
            }
        }
    }
}
