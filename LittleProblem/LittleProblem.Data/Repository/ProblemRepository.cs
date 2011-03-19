using System.Linq;
using FluentMongo.Linq;
using LittleProblem.Data.Model;
using LittleProblem.Data.Server;
using MongoDB.Bson;
using MongoDB.Driver;

namespace LittleProblem.Data.Repository
{
    class ProblemRepository : IProblemRepository
    {
        private readonly IConnexion _connexion;
        private readonly MongoCollection<Problem> _problemsCollection;

        public ProblemRepository(IConnexion conn)
        {
            _connexion = conn;
            _problemsCollection = _connexion.Collection<Problem>(CollectionNames.Problem);
        }

        public Problem GetProblem(string id)
        {
            return _problemsCollection.AsQueryable().FirstOrDefault(x => x.Id == new ObjectId(id));
        }
    }
}
