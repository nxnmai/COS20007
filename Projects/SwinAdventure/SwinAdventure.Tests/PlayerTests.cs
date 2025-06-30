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
        private Item _shovel;
        private Item _sword;

        [SetUp]
        public void Setup()
        {
            _player = new Player(new string[] { "me", "myself" }, "Mai", "the unhealthy programmer");
            _shovel = new Item(new string[] {"shovel", "spade"}, "shovel", "A sturdy shovel");
            _sword = new Item(new string[] { "sword", "blade" }, "sword", "A sharp sword");
        }

        [Test]
        public void TestPlayerIsIdentifiable()
        {
            Assert.IsTrue(_player.AreYou("me"));
            Assert.IsTrue(_player.AreYou("inventory"));
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
            located = _player.Locate("inventory");
            Assert.That(located, Is.EqualTo(_player), "Player should locate its inventory");
        }

        [Test]
        public void TestPlayerLocatesNothing()
        {
            GameObject located = _player.Locate("sword");
            Assert.IsNull(located, "Player should not locate an item not in inventory");
        }

        [Test]
        public void TestPlayerFullDescription()
        {
            _player.Inventory.Put(_shovel);
            _player.Inventory.Put(_sword);
            string expected = "You are Mai, the unhealthy programmer.\nYou are carrying:\n\ta shovel (shovel)\n\ta sword (sword)\n";
            Assert.That(_player.FullDescription, Is.EqualTo(expected), "Player's full description should include name, description, and inventory items");
        }
    }
}
