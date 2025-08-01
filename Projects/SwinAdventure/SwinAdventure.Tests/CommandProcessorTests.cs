using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SwinAdventure;

namespace SwinAdventure.Tests
{
    [TestFixture]
    public class CommandProcessorTests
    {
        private CommandProcessor _processor;
        private Player _player;
        private Location _startRoom;
        private MoveCommand _moveCommand;

        [SetUp]
        public void Setup()
        {
            _processor = new CommandProcessor();
            _startRoom = new Location(new string[] { "start" }, "Starting Room", "A cozy starting room.");
            _player = new Player(new string[] { "me", "inventory" }, "Test Player", "A test player.");
            _player.Location = _startRoom;
            _moveCommand = new MoveCommand();
            _processor.AddCommand(_moveCommand);
        }

        [Test]
        public void TestExecuteValidCommand()
        {
            string result = _processor.Execute(_player, new string[] { "move", "north" });
            Assert.That(result, Is.EqualTo("No path found for direction: north."), "Should return error for no path.");
        }

        [Test]
        public void TestExecuteUnknownCommand()
        {
            string result = _processor.Execute(_player, new string[] { "jump", "high" });
            Assert.That(result, Is.EqualTo("I don't understand the command 'jump'."), "Should return unknown command message.");
        }

        [Test]
        public void TestExecuteNoCommand()
        {
            string result = _processor.Execute(_player, new string[] { });
            Assert.That(result, Is.EqualTo("No command provided."), "Should return no command message.");
        }

        [Test]
        public void TestExecuteNullInput()
        {
            string result = _processor.Execute(_player, null);
            Assert.That(result, Is.EqualTo("No command provided."), "Should return no command message.");
        }
    }
}
