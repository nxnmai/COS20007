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
    public class IdentifiableObjectTests
    {
        private IdentifiableObject _idObject;

        [SetUp]
        public void Setup()
        {
            string studentID = "105549980";
            string firstName = "Mai";
            string familyName = "Nguyen";
            _idObject = new IdentifiableObject(new string[] {studentID, firstName, familyName });
        }

        [Test]
        public void TestAreYou()
        {
            Assert.IsTrue(_idObject.AreYou("105549980"), "Should return true for student ID");
            Assert.IsTrue(_idObject.AreYou("Mai"), "Should return true for first name");
            Assert.IsTrue(_idObject.AreYou("Nguyen"), "Should return true for family name");
        }

        [Test]
        public void TestNotAreYou()
        {
            string mismatchedID = "105549980".Replace("0", "O");
            Assert.IsFalse(_idObject.AreYou(mismatchedID), "Should return false for mismatched ID");
            Assert.IsFalse(_idObject.AreYou("Smith"), "Should return false for non-existent identifier");
        }

        [Test]
        public void TestCaseSensitive()
        {
            Assert.IsTrue(_idObject.AreYou("MAI"), "Should return true for uppercase first name");
            Assert.IsTrue(_idObject.AreYou("mai"), "Should return true for lowercase first name");
            Assert.IsTrue(_idObject.AreYou("NguyeN"), "Should return true for mixed case family name");
        }

        [Test]
        public void TestFirstID()
        {
            Assert.That(_idObject.FirstID, Is.EqualTo("105549980"), "First ID should return student ID");
        }

        [Test]
        public void TestFirstIDWithNoIDs()
        {
            var emptyObject = new IdentifiableObject(new string[] { });
            Assert.That(emptyObject.FirstID, Is.EqualTo(""), "First ID should return empty string for no identifiers");
        }

        [Test]
        public void TestAddID()
        {
            _idObject.AddIdentifier("Gwen");
            Assert.IsTrue(_idObject.AreYou("Gwen"), "Should return true for newly added identifier");
            Assert.IsTrue(_idObject.AreYou("Mai"), "Should still return true for existing identifier");
        }

        [Test]
        public void TestPrivilegeEscalation()
        {
            string correctPin = "9980";
            string studentID = "105549980";

            _idObject.PrivilegeEscalation(correctPin);
            Assert.That(_idObject.FirstID, Is.EqualTo(studentID.ToLower()), "First ID should be updated to student ID)");
            var newObject = new IdentifiableObject(new string[] {"105549980", "Mai", "Nguyen"});
            newObject.PrivilegeEscalation("1234");
            Assert.That(newObject.FirstID, Is.EqualTo("105549980"), "First ID should not change with incorrect pin");
        }
    }
}
