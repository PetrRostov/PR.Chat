using System;
using NUnit.Framework;
using PR.Chat.Test.Common;

namespace PR.Chat.Domain.Tests
{
    [TestFixture, Category(TestCategory.Domain)]
    public class RoomFixtures
    {
        
        [Test]
        public void Constructor_Should_Work()
        {
            const string name = @"Name";
            const string description = @"description";

            var room = new Room(Guid.NewGuid(), name, description, false);
            Assert.AreEqual(room.Name, name);
            Assert.AreEqual(room.Description, description);
            Assert.IsFalse(room.IsTemporary);
        }

        [Test]
        public void SameIdentityAs_should_return_true_for_equals_Names()
        {
            var room1 = new Room(Guid.NewGuid(), "naMe", "description", true);
            var room2 = new Room(Guid.NewGuid(), "naME", "efdf", false);

            Assert.IsTrue(room1.SameIdentityAs(room2));
            Assert.IsTrue(room2.SameIdentityAs(room1));
        }

        [Test]
        public void SameIdentityAs_should_return_false_for_not_equals_Names()
        {
            var room1 = new Room(Guid.NewGuid(), "naMe", "description", true);
            var room2 = new Room(Guid.NewGuid(), "name1", "efdf", false);

            Assert.IsFalse(room1.SameIdentityAs(room2));
            Assert.IsFalse(room2.SameIdentityAs(null));
        }

        [Test]
        public void GetHashcode_should_return_equals_values_for_equals_Names()
        {
            var room1 = new Room(Guid.NewGuid(), "naMe", "description", true);
            var room2 = new Room(Guid.NewGuid(), "naME", "efdf", false);

            Assert.AreEqual(room1.GetHashCode(), room2.GetHashCode());
        }

        [Test]
        public void GetHashcode_should_return_not_equals_values_for_not_equals_Names()
        {
            var room1 = new Room(Guid.NewGuid(), "naMe", "description", true);
            var room2 = new Room(Guid.NewGuid(), "naME1", "efdf", false);
            Assert.AreNotEqual(room1.GetHashCode(), room2.GetHashCode());
        }

        [Test]
        public void Equals_should_return_true_for_equals_Names()
        {
            var room1 = new Room(Guid.NewGuid(), "naMe", "description", true);
            var room2 = new Room(Guid.NewGuid(), "naME", "efdf", false);

            Assert.IsTrue(room1.Equals(room2));
        }

        [Test]
        public void Equals_should_return_false_for_not_equals_Names()
        {
            var room1 = new Room(Guid.NewGuid(), "naMe", "description", true);
            var room2 = new Room(Guid.NewGuid(), "naME4", "efdf", false);

            Assert.IsFalse(room1.Equals(room2));
            Assert.IsFalse(room1.Equals(1));
            Assert.IsFalse(room1.Equals(null));
        }

    }
}