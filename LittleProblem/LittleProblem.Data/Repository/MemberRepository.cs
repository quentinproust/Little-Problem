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
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

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

        public int GetNote(ObjectId id)
        {
            BsonJavaScript map = BsonJavaScript.Create("");
            BsonJavaScript reduce = BsonJavaScript.Create("");
            var result = _connexion.Collection<Problem>(CollectionNames.Problem)
                .MapReduce(map, reduce);

            logger.Info("Map/Reduce to compute user note has taken "+ result.Duration + ".");
            if (!result.Ok)
            {
                throw new Exception("Map reduce failed to compute user note with : " + result.ErrorMessage);
            }

            return result.Response.AsInt32;
        }
    }
}
