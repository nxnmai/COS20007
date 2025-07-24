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
    public class PathTests
    {
        private Player _player;
        private Location _startRoom;
        private Location _northRoom;
        private Path _northPath;

        [SetUp]
        public void Setup()
        {
            _startRoom = new Location(new string[] { "start" }, "Starting Room", "A cozy starting room.");
            _northRoom = new Location(new string[] { "northroom" }, "North Room", "A cold northern room.");
            _northPath = new Path(new string[] { "north" }, _northRoom);
            _player = new Player(new string[] { "me", "inventory" }, "Test Player", "A test player.");
        }

        [Test]
        public void TestPathMove()
        {
            Location initialLocation = _player.Location;
            _northPath.Move(_player);
            Assert.That(_northRoom, Is.EqualTo(_player.Location), "Player location should update to northRoom after Move.");
            Assert.That(initialLocation, Is.Not.EqualTo(_player.Location), "Player location should change after Move.");
        }
    }
}
