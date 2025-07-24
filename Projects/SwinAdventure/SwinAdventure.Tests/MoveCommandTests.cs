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
    public class MoveCommandTests
    {
        private Player _player;
        private Location _startRoom;
        private Location _northRoom;
        private Path _northPath;
        private MoveCommand _moveCommand;

        [SetUp]
        public void Setup()
        {
            _startRoom = new Location(new string[] { "start" }, "Starting Room", "A cozy starting room.");
            _northRoom = new Location(new string[] { "northroom" }, "North Room", "A cold northern room.");
            _northPath = new Path(new string[] { "north" }, _northRoom);
            _startRoom.AddPath(_northPath);
            _player = new Player(new string[] { "me", "inventory" }, "Test Player", "A test player.");
            _player.Location = _startRoom;
            _moveCommand = new MoveCommand();
        }

        [Test]
        public void TestMoveCommandValidMove()
        {
            string result = _moveCommand.Execute(_player, new string[] {"move", "north" });
            Assert.That(result, Is.EqualTo("Moved to North Room."), "Should return success message with new location.");
            Assert.That(_player.Location, Is.EqualTo(_northRoom), "Player should be moved to the north room.");
        }

        [Test]
        public void TestMoveCommandInvalidDirection()
        {
            string result = _moveCommand.Execute(_player, new string[] { "move", "south" });
            Assert.That(result, Is.EqualTo("No path found for direction: south."), "Should return error for invalid direction.");
            Assert.That(_player.Location, Is.EqualTo(_startRoom), "Player location should not change.");
        }

        [Test]
        public void TestMoveCommandInvalidInput()
        {
            string result = _moveCommand.Execute(_player, new string[] { "move" });
            Assert.That(result, Is.EqualTo("I don't know how to move like that."), "Should return error for invalid input length.");
            Assert.That(_player.Location, Is.EqualTo(_startRoom), "Player location should not change.");
        }

        [Test]
        public void TestMoveCommandWrongCommand()
        {
            string result = _moveCommand.Execute(_player, new string[] {"walk", "north" });
            Assert.That(result, Is.EqualTo("Error in move input."), "Should return error for wrong command.");
            Assert.That(_player.Location, Is.EqualTo(_startRoom), "Player location should not change.");
        }
    }
}
