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
        private readonly MongoCollection<Member> members;

        public MembershipServiceTest()
        {
            IConnexion conn = new DbConnexion("Test_LittleProblem");
            _service = new MembershipService(conn);
            members = conn.Collection<Member>("members");
        }

        [SetUp]
        public void Before()
        {
            members.RemoveAll();
        }

        [Test]
        public void UserIsCreatedOnItsFirstLogin()
        {
            var openId = "newopenid";
            var member = _service.LogIn(openId);

            Assert.That(member, Is.Not.Null);
            Assert.That(member.OpenId, Is.EqualTo(openId));

            var memberDb = members.AsQueryable().FirstOrDefault(m => m.OpenId == openId);
            Assert.That(memberDb,Is.Not.Null);
        }

        [Test]
        public void EditProfileShouldBePersisted()
        {
            var openId = "editedOpenId";
            var member = _service.LogIn(openId);

            var userName = "mimine";
            member.UserName = userName;
            var email = "bonjourmimine@gmail.com";
            member.Email = email;
            _service.EditMemberProfile(member);
            var memberDb = members.AsQueryable().FirstOrDefault(m => m.OpenId == openId);
            Assert.That(memberDb, Is.Not.Null);
            Assert.That(memberDb.UserName, Is.EqualTo(userName));
            Assert.That(memberDb.Email, Is.EqualTo(email));

        }
    }
}
