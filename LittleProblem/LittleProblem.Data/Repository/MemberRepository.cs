using System;
using System.Linq;
using FluentMongo.Linq;
using LittleProblem.Data.Model;
using LittleProblem.Data.Server;
using MongoDB.Bson;
using MongoDB.Driver;
using NLog;

namespace LittleProblem.Data.Repository
{
    public class MemberRepository : IMemberRepository
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly IConnexion _connexion;
        private readonly MongoCollection<Member> _membersCollection;

        public MemberRepository(IConnexion conn)
        {
            _connexion = conn;
            _membersCollection = _connexion.Collection<Member>(CollectionNames.Member);
        }

        public Member Get(string openId)
        {
            return _membersCollection.AsQueryable().FirstOrDefault(x => x.OpenId == openId);
        }

        public Member Get(ObjectId id)
        {
            return _membersCollection.AsQueryable().FirstOrDefault(x => x.Id == id);
        }
    }
}
