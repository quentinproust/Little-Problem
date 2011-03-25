using System;
using System.Collections.Generic;
using System.Linq;
using FluentMongo.Linq;
using LittleProblem.Data.Aggregate;
using LittleProblem.Data.Model;
using LittleProblem.Data.Server;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

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

        public List<Problem> All()
        {
            return _problemsCollection.AsQueryable()
                .OrderBy(p => p.OpenedDate)
                .Take(10)
                .ToList();
        }

        public List<Problem> All(int start)
        {
            return _problemsCollection.AsQueryable()
                .OrderBy(p => p.OpenedDate)
                .Skip(start)
                .Take(10)
                .ToList();
        }

        public ProblemAggregate Get(string id)
        {
            Problem problem = _problemsCollection.AsQueryable()
                .FirstOrDefault(x => x.Id == new ObjectId(id));

            IEnumerable<ObjectId> userIds = problem.Responses.Select(x => x.UserId);

            var bsonObjectIds = userIds.Select(x => new BsonObjectId(x)).ToArray();
            var query = Query.In("_id", bsonObjectIds);
            var members = _connexion.Collection<Member>(CollectionNames.Member)
                .Find(query)
                .ToList();

            List<ResponseAggregate> responses =
                problem.Responses.Select(res => new ResponseAggregate {
                    Text = res.Text,
                    Note = res.Note,
                    Submitter =
                        members.Where(m => m.Id == res.UserId).
                        FirstOrDefault()
                }).ToList();

            return new ProblemAggregate {
                                    Id = problem.Id,
                                    OpenedDate = problem.OpenedDate,
                                    Text = problem.Text,
                                    Title = problem.Title,
                                    Responses = responses
                                };
        }
    }
}
