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
    public class LookCommandTests
    {
        private Player _player;
        private Item _gem;
        private Bag _bag;
        private Item _coin;
        private LookCommand _lookCommand;

        [SetUp]
        public void Setup()
        {
            _player = new Player(new string[] { "me", "myself" }, "hero", "an unhealthy programmer");
            _gem = new Item(new string[] { "gem", "ruby" }, "red gem", "A shiny red ruby");
            _bag = new Bag(new string[] { "bag", "sack" }, "small bag", "A small leather bag");
            _coin = new Item(new string[] { "coin", "gold" }, "gold coin", "A shiny gold coin");
            _lookCommand = new LookCommand();
        }

        [Test]
        public void TestLookAtMe()
        {
            string result = _lookCommand.Execute(_player, new string[] { "look", "at", "me" });
            Assert.That(result, Is.EqualTo("You are hero, an unhealthy programmer.\nYou are carrying:\nIn the inventory you can see: Your personal inventory.\n"), "Should return the player's full description when looking at 'me'");
        }

        [Test]
        public void TestLookAtGem()
        {
            _player.Inventory.Put(_gem);
            string result = _lookCommand.Execute(_player, new string[] { "look", "at", "gem" });
            Assert.That(result, Is.EqualTo("A shiny red ruby"), "Should return gem's description when looking at a gem in player's inventory");
        }

        [Test]
        public void TestLookAtUnk()
        {
            string result = _lookCommand.Execute(_player, new string[] { "look", "at", "gem" });
            Assert.That(result, Is.EqualTo("I cannot find the <gem> in the hero"), "Should return 'cannot find' when player has no gem");
        }

        [Test]
        public void TestLookAtGemInMe()
        {
            _player.Inventory.Put(_gem);
            string result = _lookCommand.Execute(_player, new string[] { "look", "at", "gem", "in", "inventory" });
            Assert.That(result, Is.EqualTo("A shiny red ruby"), "Should return gem's description when looking at 'gem in inventory'");

            result = _lookCommand.Execute(_player, new string[] { "look", "at", "gem", "in", "me" });
            Assert.That(result, Is.EqualTo("A shiny red ruby"), "Should return gem's description when looking at 'gem in me'");
        }

        [Test]
        public void TestLookAtGemInBag()
        {
            _bag.Inventory.Put(_gem);
            _player.Inventory.Put(_bag);
            string result = _lookCommand.Execute(_player, new string[] { "look", "at", "gem", "in", "bag" });
            Assert.That(result, Is.EqualTo("A shiny red ruby"), "Should return gem's description when looking at a gem in a bag");
        }

        [Test]
        public void TestLookAtGemInNoBag()
        {
            string result = _lookCommand.Execute(_player, new string[] {"look", "at", "gem", "in", "bag"});
            Assert.That(result, Is.EqualTo("I cannot find the <bag>"), "Should return 'cannot find bag' when player has no bag");
        }

        [Test]
        public void TestLookAtNoGemInBag()
        {
            _player.Inventory.Put(_bag);
            string result = _lookCommand.Execute(_player, new string[] { "look", "at", "gem", "in", "bag" });
            Assert.That(result, Is.EqualTo("I cannot find the <gem> in the small bag"), "Should return 'cannot find gem' when bag has no gem");
        }

        [Test]
        public void TestInvalidLook()
        {
            string[][] invalidInputs = new string[][]
            {
                new string[] { "look" },
                new string[] { "look", "at" },
                new string[] { "look", "at", "gem", "at", "bag" },
                new string[] { "hello" },
                new string[] { "look", "around" }
            };
            foreach (var input in invalidInputs)
            {
                string result = _lookCommand.Execute(_player, input);
                Assert.That(result, Is.AnyOf("I don't know how to look like that", "Error in look input", "What do you want to look at?", "What do you want to look in?"), $"Should handle invalid input: {string.Join(" ", input)}");
            }
        }
    }
}
    