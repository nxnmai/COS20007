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
    public class InventoryTests
    {
        private Inventory _inventory;
        private Item _shovel;
        private Item _sword;

        [SetUp]
        public void Setup()
        {
            _inventory = new Inventory();
            _shovel = new Item(new string[] { "shovel", "spade" }, "shovel", "A sturdy shovel");
            _sword = new Item(new string[] { "sword", "blade" }, "sword", "A sharp sword");
        }

        [Test]
        public void TestFindItem()
        {
            _inventory.Put(_shovel);
            Assert.IsTrue(_inventory.HasItem("shovel"), "Inventory should have shovel");
            Assert.IsTrue(_inventory.HasItem("spade"), "Inventory should have spade");
            Assert.IsFalse(_inventory.HasItem("sword"), "Inventory should not have sword");
        }

        [Test]
        public void TestNoItemFind()
        {
            Assert.IsFalse(_inventory.HasItem("shovel"), "Inventory should not have shovel before adding it");
        }

        [Test]
        public void TestFetchItem()
        {
            _inventory.Put(_shovel);
            Item fetched = (Item)_inventory.Fetch("shovel");
            Assert.That(fetched, Is.EqualTo(_shovel), "Fetched item should be the shovel");
            Assert.IsTrue(_inventory.HasItem("shovel"), "Inventory should still have the shovel after fetching it");
        }

        [Test]
        public void TestTakeItem()
        {
            _inventory.Put(_shovel);
            Item taken = (Item)_inventory.Take("shovel");
            Assert.That(taken, Is.EqualTo(_shovel), "Taken item should be the shovel");
            Assert.IsFalse(_inventory.HasItem("shovel"), "Inventory should not have the shovel after taking it");
        }

        [Test]
        public void TestItemList()
        {
            _inventory.Put(_shovel);
            _inventory.Put(_sword);
            string expected= "\ta shovel (shovel)\n\ta sword (sword)\n";
            Assert.That(_inventory.ItemList, Is.EqualTo(expected), "Item list should match the expected format");
        }
    }
}
