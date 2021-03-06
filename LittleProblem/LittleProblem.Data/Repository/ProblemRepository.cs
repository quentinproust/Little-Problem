﻿using System.Collections.Generic;
using System.Linq;
using FluentMongo.Linq;
using LittleProblem.Common.CustomException;
using LittleProblem.Data.Aggregate;
using LittleProblem.Data.Model;
using LittleProblem.Data.Server;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace LittleProblem.Data.Repository
{
    public class ProblemRepository : IProblemRepository
    {
        private readonly IConnexion _connexion;
        private readonly MongoCollection<Problem> _problemsCollection;

        public ProblemRepository(IConnexion conn)
        {
            _connexion = conn;
            _problemsCollection = _connexion.Collection<Problem>(CollectionNames.Problem);
        }

        public static int PageSize
        {
            get { return 10; }
        }

        public IEnumerable<Problem> All(int start)
        {
            return _problemsCollection.AsQueryable()
                .OrderByDescending(x => x.OpenedDate)
                .Skip(start * PageSize)
                .Take(PageSize)
                .ToList();
        }

        public int Count()
        {
            return _problemsCollection.AsQueryable()
                .Count();
        }

        public ProblemAggregate Get(string id)
        {
            Problem problem = _problemsCollection.AsQueryable()
                .FirstOrDefault(x => x.Id == new ObjectId(id));

            if (problem == null)
            {
                throw new EntityNotFoundException("Could not find the requested problem. Problem Id was :" + id);
            }

            List<ObjectId> userIds = problem.Responses.Select(x => x.UserId).ToList();
            userIds.Add(problem.UserId);

            var bsonObjectIds = userIds.Select(x => new BsonObjectId(x)).ToArray();
            var query = Query.In("_id", bsonObjectIds);
            var members = _connexion.Collection<Member>(CollectionNames.Member)
                .Find(query)
                .ToList();

            List<ResponseAggregate> responses =
                problem.Responses.Select(res => new ResponseAggregate {
                    Id = res.Id.ToString(),
                    Text = res.Text,
                    Note = res.Note,
                    Submitter = members.Where(m => m.Id == res.UserId).FirstOrDefault()
                }).ToList();

            return new ProblemAggregate {
                                    Id = problem.Id.ToString(),
                                    OpenedDate = problem.OpenedDate,
                                    Text = problem.Text,
                                    Title = problem.Title,
                                    Responses = responses,
                                    IsClosed = problem.IsClosed(),
                                    Submitter = members.Where(m => m.Id == problem.UserId).First()
                                };
        }
    }
}
