using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentMongo.Linq;
using LittleProblem.Data.Model;
using LittleProblem.Data.Server;
using MongoDB.Bson;
using MongoDB.Driver;

namespace LittleProblem.Data.Services
{
    public class ProblemService : IProblemService
    {

        private readonly IConnexion _connexion;
        private readonly MongoCollection<Problem> _problemsCollection;

        public ProblemService(IConnexion connexion)
        {
            _connexion = connexion;
            _problemsCollection = _connexion.Collection<Problem>(CollectionNames.Problem);
        }

        public Problem CreateProblem(string title, string text, Member member)
        {
            var problem = new Problem
                                  {
                                      UserId = member.Id,
                                      Text = text,
                                      OpenedDate = DateTime.Now,
                                      Title = title
                                  };
            _problemsCollection.Save(problem);
            return problem;
        }

        public void AddResponse(string problemId, string text, Member member)
        {
            var problem = _problemsCollection.AsQueryable()
                .FirstOrDefault(m => m.Id == new ObjectId(problemId));
            problem.Responses.Add(new Response
                                      {
                                          Text = text,
                                          UserId = member.Id
                                      });
            _problemsCollection.Save(problem);
        }

        public void CloseProblem(string problemId, Member member)
        {
            var problem = _problemsCollection.AsQueryable()
                .FirstOrDefault(m => m.Id == new ObjectId(problemId));
            if (member.Id.Equals(problem.UserId))
            {
                problem.ClosureDate = DateTime.Now;
                _problemsCollection.Save(problem);
            } else
            {
                throw new AccessViolationException("Only the problem create can close it");
            }
        }

        public void ValidateAsASolution(string problemId, Member member, Response response)
        {
            CloseProblem(problemId, member);
            NoteResponse(problemId, response.Id.ToString(), member, +1);
        }

        public void DownResponse(string problemId, string responseId, Member member)
        {
            NoteResponse(problemId, responseId, member, -1);
        }

        public void UpResponse(string problemId, string responseId, Member member)
        {
            NoteResponse(problemId, responseId, member, +1);
        }

        private void NoteResponse(string problemId, string responseId, Member member, int note)
        {
            var problem = _problemsCollection.AsQueryable()
                .FirstOrDefault(m => m.Id == new ObjectId(problemId));
            var response = problem.Responses.Where(r => r.Id == new ObjectId(responseId)).First();
            if (response.UserId.Equals(member.Id))
            {
                throw new AccessViolationException("You can't note yourself");
            }
            response.Note += note;
            _problemsCollection.Save(problem);
        }
    }
}
