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
    public class ItemTests
    {
        private Item _item;

        [SetUp]
        public void Setup()
        {
            _item = new Item(new string[] { "shovel", "spade" }, "shovel", "A sturdy shovel");
        }

        [Test]
        public void TestItemIsIdentifiable()
        {
            Assert.IsTrue(_item.AreYou("shovel"), "Item should be identifiable by 'shovel'");
            Assert.IsTrue(_item.AreYou("spade"), "Item should be identifiable by 'spade'");
            Assert.IsFalse(_item.AreYou("sword"), "Item should not be identifiable by 'sword'");
        }

        [Test]
        public void TestShortDescription()
        {
            Assert.That(_item.ShortDescription, Is.EqualTo("a shovel (shovel)"), "Short description should be 'a shovel (shovel)'");
        }

        [Test]
        public void TestFullDescription()
        {
            Assert.That(_item.FullDescription, Is.EqualTo("A sturdy shovel"), "Full description should be 'A sturdy shovel'");
        }

        [Test]
        public void TestPrivilegeEscalation()
        {
            string correctPin = "9980";
            string studentID = "105549980";

            _item.PrivilegeEscalation(correctPin);
            Assert.That(_item.FirstID, Is.EqualTo(studentID.ToLower()), "First ID should be updated to Student ID after privilege escalation");

            Item newItem = new Item(new string[] { "shovel", "spade" }, "shovel", "A sturdy shovel");
            newItem.PrivilegeEscalation("1234"); // Incorrect pin
            Assert.That(newItem.FirstID, Is.EqualTo("shovel"), "First ID should not change with incorrect pin");
        }

    }
}
