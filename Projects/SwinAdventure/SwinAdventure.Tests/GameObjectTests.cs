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
    public class GameObjectTests
    {
        private class TestGameObject : GameObject
        {
            public TestGameObject(string[] idents, string name, string desc) : base(idents, name, desc)
            {
            }
        }

        private TestGameObject _gameObject;

        [SetUp]
        public void Setup()
        {
            _gameObject = new TestGameObject(new string[] { "test", "object" }, "test object", "A test object description");
        }

        [Test]
        public void TestName()
        {
            Assert.That(_gameObject.Name, Is.EqualTo("test object"), "Name should be 'test object'");
        }

        [Test]
        public void TestDescription()
        {
            Assert.That(_gameObject.Description, Is.EqualTo("A test object description"), "Description should be 'A test object description'");
        }
        
        [Test]
        public void TestShortDescription()
        {
            Assert.That(_gameObject.ShortDescription, Is.EqualTo("a test object (test)"), "Short description should be 'a test object (test)'");
        }

        [Test]
        public void TestFullDescription()
        {
            Assert.That(_gameObject.FullDescription, Is.EqualTo("A test object description"), "Full description should be 'A test object description'");
        }
    }
}
