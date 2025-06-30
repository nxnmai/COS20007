using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SwinAdventure;

namespace SwinAdventure.Tests
{
    [TestFixture]
    public class BagTests
    {
        private Bag _bag;
        private Item _gem;
        private Bag _innerBag;
        private Item _coin;

        [SetUp]
        public void Setup()
        {
            _bag = new Bag(new string[] { "bag", "sack" }, "leather bag", "A small leather bag");
            _gem = new Item(new string[] { "gem", "ruby" }, "red gem", "A shiny red ruby");
            _innerBag = new Bag(new string[] { "small bag", "pouch" }, "small pouch", "A tiny pouch");
            _coin = new Item(new string[] { "coin", "gold" }, "gold coin", "A shiny gold coin");
        }

        [Test]
        public void TestBagLocatesItems()
        {
            _bag.Inventory.Put(_gem);
            GameObject located = _bag.Locate("gem");

            Assert.That(located, Is.EqualTo(_gem), "Bag should locate the gem in its inventory");
            Assert.IsTrue(_bag.Inventory.HasItem("gem"), "Item should still be in the bag's inventory after locating");
        }

        [Test]
        public void TestBagLocatesItself()
        {
            GameObject located = _bag.Locate("bag");
            Assert.That(located, Is.EqualTo(_bag), "Bag should return itself when located by 'bag'");

            located = _bag.Locate("sack");
            Assert.That(located, Is.EqualTo(_bag), "Bag should return itself when located by 'sack'");
        }

        [Test]
        public void TestBagLocatesNothing()
        {
            GameObject located = _bag.Locate("coin");
            Assert.IsNull(located, "Bag should return null for an item it does not contain");
        }

        [Test]
        public void TestBagFullDescription()
        {
            _bag.Inventory.Put(_gem);
            string expected = "In the leather bag you can see: A small leather bag.\n\ta red gem (gem)\n";

            Assert.That(_bag.FullDescription, Is.EqualTo(expected), "Bag's full description should include 'In the <name> you can see: ' and the item list");
        }

        [Test]
        public void TestBagInBag()
        {
            _innerBag.Inventory.Put(_coin);
            _bag.Inventory.Put(_innerBag);

            GameObject located = _bag.Locate("small bag");
            Assert.That(located, Is.EqualTo(_innerBag), "Bag should locate the inner bag");

            _bag.Inventory.Put(_gem);
            located = _bag.Locate("gem");
            Assert.That(located, Is.EqualTo(_gem), "Bag should locate the gem in its own inventory");

            located = _bag.Locate("coin");
            Assert.IsNull(located, "Bag should not locate items in the inner bag");
        }

        [Test]
        public void TestBagInBagPrivilegedItem()
        {
            _bag.Inventory.Put(_innerBag);
            _coin.PrivilegeEscalation("9980");
            _innerBag.Inventory.Put(_coin);
            GameObject located = _bag.Locate("coin");

            Assert.IsNull(located, "Bag cannot locate the privileged item in inner bag");
        }
    }
}
