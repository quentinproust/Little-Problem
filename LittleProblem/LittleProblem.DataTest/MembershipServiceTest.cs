using System.Linq;
using FluentMongo.Linq;
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
            _members = conn.Collection<Member>("members");
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
    }
}
