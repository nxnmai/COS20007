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
    public class LocationTests
    {
        private Location _location;
        private Item _item;
        private Player _player;
        private Location _northRoom;
        private Path _northPath;

        [SetUp]
        public void Setup()
        {
            _location = new Location(new string[] { "room", "hall" }, "great hall", "A grand hall with high ceilings");
            _item = new Item(new string[] { "gem", "ruby" }, "red gem", "A shiny red ruby");
            _location.Inventory.Put(_item);
            _player = new Player(new string[] { "me", "myself" }, "Mai", "the unhealthy programmer");
            _player.Location = _location;
            _northRoom = new Location(new string[] { "northroom" }, "North Room", "A cold northern room.");
            _northPath = new Path(new string[] { "north" }, _northRoom);
            _location.AddPath(_northPath);
        }

        [Test]
        public void TestLocationIdentifiesItself()
        {
            Assert.IsTrue(_location.AreYou("room"));
            Assert.IsTrue(_location.AreYou("hall"));
            Assert.IsFalse(_location.AreYou("bathroom"));
        }

        [Test]
        public void TestLocationLocatesItems()
        {
            GameObject result = _location.Locate("gem");
            Assert.That(result, Is.EqualTo(_item));
        }

        [Test]
        public void TestLocationLocatesItself()
        {
            GameObject result = _player.Locate("room");
            Assert.That(result, Is.EqualTo(_location));
            result = _location.Locate("hall");
            Assert.That(result, Is.EqualTo(_location));
        }

        [Test]
        public void TestLocationFullDescription()
        {
            string expected = "You are in great hall, A grand hall with high ceilings.\nYou can see:\n\ta red gem (gem)\n";
            Assert.That(expected, Is.EqualTo(_location.FullDescription));
        }

        [Test]
        public void TestLocationNoItemsDescription()
        {
            Location emptyLocation = new Location(new string[] {"cave"}, "dark cave", "A spooky cave");
            string expected = "You are in dark cave, A spooky cave.\n";
            Assert.That(expected, Is.EqualTo(emptyLocation.FullDescription));
        }

        [Test]
        public void TestLocationLocatePath()
        {
            Path foundPath = _location.LocatePath("north");
            Assert.That(foundPath, Is.EqualTo(_northPath), "Should find the north path.");
            Assert.IsNull(_location.LocatePath("south"), "Should retun null for non-existent path.");
        }
    }
}
