using System;
using System.Linq;
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
            ValidateMember(member);
            ValidateProblemInformations(title, text);

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
            CloseProblemWithSolution(problemId, member, null);
        }

        public void ValidateAsASolution(string problemId, Member member, Response response)
        {
            CloseProblemWithSolution(problemId, member, response);
            NoteResponse(problemId, response.Id.ToString(), member, +1);
        }

        private void CloseProblemWithSolution(string problemId, Member member, Response solution)
        {
            var problem = _problemsCollection.AsQueryable()
                .FirstOrDefault(m => m.Id == new ObjectId(problemId));
            if (member.Id.Equals(problem.UserId))
            {
                problem.ClosureDate = DateTime.Now;
                problem.CurrentSolution = solution;
                _problemsCollection.Save(problem);
            }
            else
            {
                throw new AccessViolationException("Only the problem create can close it");
            }
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
                throw new Exception("You can't note yourself");
            }
            response.Note += note;
            _problemsCollection.Save(problem);
        }

        private void ValidateProblemInformations(String title, String description)
        {
            if (String.IsNullOrEmpty(title))
            {
                throw new ArgumentException("The title can't be empty");
            }
            
            if (String.IsNullOrEmpty(description))
            {
                throw new ArgumentException("The description can't be empty");
            }

            var existingProblem = _problemsCollection.AsQueryable().Where(p => p.Title == title).FirstOrDefault();
            if (existingProblem != null)
            {
                throw new ArgumentException("A problem with the same title already exists.");
            }
        }

        private void ValidateMember(Member member)
        {
            if (member == null)
            {
                throw new ArgumentException("It require a member so null values are not accepted.");
            }
            var existingMember = _connexion.Collection<Member>(CollectionNames.Member)
                .AsQueryable().Where(m => m.Id == member.Id).FirstOrDefault();
            if (existingMember == null)
            {
                throw new ArgumentException("It requiere an existing member already persisted in db.");
            }
        }
    }
}
