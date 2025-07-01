using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwinAdventure
{
    public class IdentifiableObject
    {
        private List<string> _identifiers;

        public IdentifiableObject(string[] idents)
        {
            _identifiers = new List<string>();
            foreach (string id in idents)
            {
                if (!string.IsNullOrEmpty(id))
                {
                    _identifiers.Add(id.ToLower());
                }
            }
        }

        public bool AreYou(string id)
        {
            if (string.IsNullOrEmpty(id))
                return false;
            return _identifiers.Contains(id.ToLower());
        }

        public void AddIdentifier(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                _identifiers.Add(id.ToLower());
            }
        }

        public void PrivilegeEscalation(string pin)
        {
            string studentID = "105549980";
            string last4digits = "9980";

            if (pin == last4digits && _identifiers.Count > 0)
            {
                _identifiers[0] = studentID.ToLower();
            }
        }

        public string FirstID
        {
            get
            {
                return _identifiers.Count > 0 ? _identifiers[0] : "";
            }
        }
    }
}
