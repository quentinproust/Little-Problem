using System;
using System.Linq;
using FluentMongo.Linq;
using LittleProblem.Data;
using LittleProblem.Data.Model;
using LittleProblem.Data.Server;
using LittleProblem.Data.Services;
using MongoDB.Driver;
using NUnit.Framework;

namespace LittleProblem.DataTest
{
    [TestFixture]
    class MembershipServiceTest
    {
        private readonly IMembershipService _service;
        private readonly MongoCollection<Member> _members;

        public MembershipServiceTest()
        {
            IConnexion conn = new DbConnexion("Test_LittleProblem");
            _service = new MembershipService(conn);
            _members = conn.Collection<Member>(CollectionNames.Member);
        }

        [SetUp]
        public void Before()
        {
            _members.RemoveAll();
        }

        [Test]
        public void UserIsCreatedOnItsFirstLogin()
        {
            const string openId = "newopenid";
            var member = _service.LogIn(openId);

            Assert.That(member, Is.Not.Null);
            Assert.That(member.OpenId, Is.EqualTo(openId));

            var memberDb = _members.AsQueryable().FirstOrDefault(m => m.OpenId == openId);
            Assert.That(memberDb,Is.Not.Null);
        }

        [Test]
        public void UserLogInWithExistingProfile()
        {
            const string openId = "newopenid";
            var member = _service.LogIn(openId); // New member is created
            var existingMember = _service.LogIn(openId); // New member is created

            Assert.That(member, Is.Not.Null);
            Assert.That(existingMember, Is.Not.Null);
            Assert.That(member.OpenId, Is.EqualTo(openId));
            Assert.That(existingMember.OpenId, Is.EqualTo(openId));

            Assert.That(existingMember, Is.EqualTo(member));
        }

        [Test]
        public void UpdateLastConnectionDateWhenUserLogIn()
        {
            const string openId = "newopenid";
            var member = _service.LogIn(openId); // New member is created
            member.LastConnection = DateTime.Now.Subtract(TimeSpan.FromDays(10));
            _members.Save(member);

            var existingMember = _service.LogIn(openId);
            Assert.That(existingMember.LastConnection, Is.Not.EqualTo(member.LastConnection));
        }

        [Test]
        public void EditProfileShouldBePersisted()
        {
            const string openId = "editedOpenId";
            const string userName = "mimine";
            const string email = "bonjourmimine@gmail.com";

            var member = _service.LogIn(openId);

            member.UserName = userName; // Modifying member.
            member.Email = email;

            _service.EditMemberProfile(member);
            var memberDb = _members.AsQueryable().FirstOrDefault(m => m.OpenId == openId);
            Assert.That(memberDb, Is.Not.Null);
            Assert.That(memberDb.UserName, Is.EqualTo(userName));
            Assert.That(memberDb.Email, Is.EqualTo(email));
        }

        [Test]
        public void UpdateUserNote()
        {
            _service.UpdateUserNote(); // Don't know how to test this
            // Generaly means I have a design problem.
        }
    }
}
