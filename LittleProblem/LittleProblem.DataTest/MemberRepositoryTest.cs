using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LittleProblem.Data;
using LittleProblem.Data.Model;
using LittleProblem.Data.Repository;
using LittleProblem.Data.Server;
using LittleProblem.Data.Services;
using MongoDB.Driver;
using NUnit.Framework;

namespace LittleProblem.DataTest
{
    [TestFixture]
    class MemberRepositoryTest
    {
        private readonly IMemberRepository _memberRepository;
        private readonly MongoCollection<Member> members;

        public MemberRepositoryTest()
        {
            IConnexion conn = new DbConnexion("Test_LittleProblem");
            _memberRepository = new MemberRepository(conn);
            members = conn.Collection<Member>("members");
        }

        [SetUp]
        public void Before()
        {
            members.RemoveAll();
        }

        [Test]
        public void GetAMemberFromItsOpenId()
        {
            string openId = "testuser.openid.com";
            string userName = "UserTest";

            members.Insert(new Member()
                               {
                                   OpenId = openId,
                                   UserName = userName
                               });
            Member result = _memberRepository.Get(openId);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.OpenId, Is.EqualTo(openId));
            Assert.That(result.UserName, Is.EqualTo(userName));
        }
    }
}
