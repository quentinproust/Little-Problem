using LittleProblem.Data;
using LittleProblem.Data.Model;
using LittleProblem.Data.Repository;
using LittleProblem.Data.Server;
using MongoDB.Bson;
using MongoDB.Driver;
using NUnit.Framework;

namespace LittleProblem.DataTest
{
    [TestFixture]
    class MemberRepositoryTest
    {
        private readonly IMemberRepository _memberRepository;
        private readonly MongoCollection<Member> _members;

        public MemberRepositoryTest()
        {
            IConnexion conn = new DbConnexion("Test_LittleProblem");
            _memberRepository = new MemberRepository(conn);
            _members = conn.Collection<Member>(CollectionNames.Member);
        }

        [SetUp]
        public void Before()
        {
            _members.RemoveAll();
        }

        [Test]
        public void GetAMemberFromItsOpenId()
        {
            const string openId = "testuser.openid.com";
            const string userName = "UserTest";

            _members.Insert(new Member
                                {
                                   OpenId = openId,
                                   UserName = userName
                               });
            Member result = _memberRepository.Get(openId);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.OpenId, Is.EqualTo(openId));
            Assert.That(result.UserName, Is.EqualTo(userName));
        }

        [Test]
        public void GetNullWhenOpenIdDoesNotExist()
        {
            const string openId = "testuser.openid.com";
            Member result = _memberRepository.Get(openId);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void GetAMemberFromItsMongoId()
        {
            const string openId = "testuser.openid.com.test";
            const string userName = "UserTest";

            var objectId = ObjectId.GenerateNewId();
            _members.Insert(new Member
                                {
                Id = objectId,
                OpenId = openId,
                UserName = userName
            });
            Member result = _memberRepository.Get(objectId);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(objectId));
            Assert.That(result.OpenId, Is.EqualTo(openId));
            Assert.That(result.UserName, Is.EqualTo(userName));
        }

        [Test]
        public void GetNullIfMongoIdDoesNotExist()
        {
            var objectId = ObjectId.GenerateNewId();
            Member result = _memberRepository.Get(objectId);

            Assert.That(result, Is.Null);
        }
    }
}
