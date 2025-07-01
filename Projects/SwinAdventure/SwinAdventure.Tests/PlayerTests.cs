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
    public class PlayerTests
    {
        private Player _player;
        private Location _location;
        private Item _item;
        private Item _shovel;
        private Item _sword;

        [SetUp]
        public void Setup()
        {
            _player = new Player(new string[] { "me", "myself" }, "Mai", "the unhealthy programmer");
            _shovel = new Item(new string[] { "shovel", "spade" }, "shovel", "A sturdy shovel");
            _sword = new Item(new string[] { "sword", "blade" }, "sword", "A sharp sword");
            _location = new Location(new string[] { "room" }, "great hall", "A grand hall");
            _item = new Item(new string[] { "gem" }, "red gem", "A shiny red ruby");
            _location.Inventory.Put(_item);
            _player.Location = _location;
        }

        [Test]
        public void TestPlayerLocatesItemsInLocation()
        {
            GameObject result = _player.Locate("gem");
            Assert.That(result, Is.EqualTo(_item), "Player should be able to locate the gem in the current location.");
        }

        [Test]
        public void TestPlayerLocatesLocation()
        {
            GameObject result = _player.Locate("room");
            Assert.That(result, Is.EqualTo(_location), "Player should be able to locate the current location.");
        }

        [Test]
        public void TestPlayerLocatesNothing()
        {
            GameObject result = _player.Locate("sword");
            Assert.IsNull(result, "Player should not be able to locate an item that is not in the inventory or location.");
        }

        [Test]
        public void TestPlayerIsIdentifiable()
        {
            Assert.IsTrue(_player.AreYou("me"));
            Assert.IsTrue(_player.AreYou("myself"));
            Assert.IsFalse(_player.AreYou("shovel"));
        }

        [Test]
        public void TestPlayerLocatesItems()
        {
            _player.Inventory.Put(_shovel);
            GameObject located = _player.Locate("shovel");
            Assert.That(located, Is.EqualTo(_shovel), "Player should be able to locate the shovel in inventory");
            Assert.IsTrue(_player.Inventory.HasItem("shovel"), "Player's inventory should still have the shovel after locating it");
        }

        [Test]
        public void TestPlayerLocatesItself()
        {
            GameObject located = _player.Locate("me");
            Assert.That(located, Is.EqualTo(_player), "Player should locate itself");
            located = _player.Locate("myself");
            Assert.That(located, Is.EqualTo(_player), "Player should locate itself");
        }

        [Test]
        public void TestPlayerFullDescription()
        {
            _player.Inventory.Put(_shovel);
            _player.Inventory.Put(_sword);
            string expected = "You are Mai, the unhealthy programmer.\nYou are carrying:\nIn the inventory you can see: Your personal inventory.\n\ta shovel (shovel)\n\ta sword (sword)\n";
            Assert.That(expected, Is.EqualTo(_player.FullDescription), "Player's full description should include name, description, and inventory items");
        }
    }
}
