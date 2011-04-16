using System;
using System.Linq;
using AutoPoco;
using AutoPoco.DataSources;
using AutoPoco.Engine;
using FluentMongo.Linq;
using LittleProblem.Common.Colllections;
using LittleProblem.Data;
using LittleProblem.Data.Model;
using LittleProblem.Data.Server;
using LittleProblem.Data.Services;
using MongoDB.Driver;
using NUnit.Framework;

namespace LittleProblem.DataTest
{
    [TestFixture]
    public class ProblemServiceTest
    {
        private readonly IConnexion _conn;
        private readonly MongoCollection<Problem> _problemCollection;
        private readonly IProblemService _problemService;
        private readonly IGenerationSession _session;

        public ProblemServiceTest()
        {
            _conn = new DbConnexion("Test_LittleProblem");
            _problemCollection = _conn.Collection<Problem>(CollectionNames.Problem);
            _problemService = new ProblemService(_conn);

            // Perform factory set up (once for entire test run)
            IGenerationSessionFactory factory = AutoPocoContainer.Configure(x =>
            {
                x.Conventions(c => c.UseDefaultConventions());
                x.AddFromAssemblyContainingType<Member>();

                x.Include<Member>()
                    .Setup(m => m.Id).Use<ObjectIdDataSource>()
                    .Setup(m => m.OpenId).Random(5, 10)
                    .Setup(m => m.UserName).Random(5, 7);

                x.Include<Problem>()
                    .Setup(p => p.Id).Use<ObjectIdDataSource>()
                    .Setup(p => p.Text).Use<LoremIpsumSource>()
                    .Setup(p => p.Title).Random(7, 12);

                x.Include<Response>()
                    .Setup(r => r.Text).Use<LoremIpsumSource>();
            });

            // Generate one of these per test (factory will be a static variable most likely)
            _session = factory.CreateSession();
        }

        [SetUp]
        public void SetUp()
        {
            _problemCollection.RemoveAll();
        }

        [Test]
        public void CreateProblemWillSaveItInDb()
        {
            var member = _session.Single<Member>().Get();
            _conn.Collection<Member>(CollectionNames.Member).Save(member);

            const string title = "New Problem Created for test";
            const string description = "Some description";
            var problem = _problemService.CreateProblem(title, description, member);
            Assert.That(problem, Is.Not.Null);
            Assert.That(problem.Id, Is.Not.Null);
            Assert.That(problem.Title, Is.EqualTo(title));
            Assert.That(problem.Text, Is.EqualTo(description));
            Assert.That(problem.Responses, Is.Empty);

            var fromDb = _problemCollection.AsQueryable().Where(x => x.Id == problem.Id).First();
            Assert.That(fromDb, Is.Not.Null);
            Assert.That(fromDb.Id, Is.Not.Null);
            Assert.That(fromDb.Id, Is.EqualTo(problem.Id));
            Assert.That(fromDb.Title, Is.EqualTo(problem.Title));
            Assert.That(fromDb.Text, Is.EqualTo(problem.Text));
            Assert.That(fromDb.Responses, Is.Empty);
        }

        [Test]
        public void CreateProblemRequireAnExistingMember()
        {
            // No null
            Assert.That(
                () => _problemService.CreateProblem("some title", "desc", null),
                Throws.ArgumentException);

            // The member has to be an existing member in db.
            var member = _session.Single<Member>().Get();
            Assert.That(
                () => _problemService.CreateProblem("some title", "desc", member),
                Throws.ArgumentException);
        }

        [Test]
        public void CreateProblemRequireTextAndDescription()
        {
            var member = _session.Single<Member>().Get();
            _conn.Collection<Member>(CollectionNames.Member).Save(member);

            Assert.That(
                () => _problemService.CreateProblem("some title", String.Empty, member),
                Throws.ArgumentException);

            Assert.That(
                () => _problemService.CreateProblem(String.Empty, "desc", member),
                Throws.ArgumentException);
        }

        [Test]
        public void CantCreateTwoProblemWithSameTitle()
        {
            var member = _session.Single<Member>().Get();
            _conn.Collection<Member>(CollectionNames.Member).Save(member);

            const string title = "SameTitleForTheseTwoProblem";

            _problemService.CreateProblem(title, "some description", member);

            Assert.That(
                () => _problemService.CreateProblem(title, "other description", member),
                Throws.ArgumentException);
        }

        [Test]
        public void AddResponseToProblem()
        {
            var member = _session.Single<Member>().Get();
            _conn.Collection<Member>(CollectionNames.Member).Save(member);

            var problem = _problemService.CreateProblem("Problem 3244", "Some description", member);

            var stringSource = new RandomStringSource(5, 20);
            for (int i = 0; i < 10; i++)
            {
                _problemService.AddResponse(problem.Id.ToString(), stringSource.Next(_session), member);
            }

            // Get responses from db and check them.
            var fromDb = _problemCollection.AsQueryable().Where(p => p.Id == problem.Id).First();
            Assert.That(fromDb.Responses.Count, Is.EqualTo(10));
            for (int i = 0; i < 10; i++)
            {
                Assert.That(fromDb.Responses[i].UserId, Is.EqualTo(member.Id));
            }
        }

        [Test]
        public void NoteAsAGoodResponse()
        {
            var memberCreatingResponse = _session.Single<Member>().Get();
            var memberVoting = _session.Single<Member>().Get();
            _conn.Collection<Member>(CollectionNames.Member).Save(memberCreatingResponse);

            var problem = _problemService.CreateProblem("Problem 3244", "Some description", memberCreatingResponse);
            _problemService.AddResponse(problem.Id.ToString(), "Some solution", memberCreatingResponse);

            // Get the response from db
            var fromDb = _problemCollection.AsQueryable().Where(p => p.Id == problem.Id).First();
            var response = fromDb.Responses.First();

            _problemService.UpResponse(problem.Id.ToString(), response.Id.ToString(), memberVoting);

            // Reload to check if response note has changed.
            fromDb = _problemCollection.AsQueryable().Where(p => p.Id == problem.Id).First();
            var responseChanged = fromDb.Responses.First();

            Assert.That(responseChanged.Note, Is.EqualTo(response.Note + 1));
        }

        [Test]
        public void NoteAsABadResponse()
        {
            var memberCreatingResponse = _session.Single<Member>().Get();
            var memberVoting = _session.Single<Member>().Get();
            _conn.Collection<Member>(CollectionNames.Member).Save(memberCreatingResponse);

            var problem = _problemService.CreateProblem("Problem 3244", "Some description", memberCreatingResponse);
            _problemService.AddResponse(problem.Id.ToString(), "Some solution", memberCreatingResponse);

            // Get the response from db
            var fromDb = _problemCollection.AsQueryable().Where(p => p.Id == problem.Id).First();
            var response = fromDb.Responses.First();

            _problemService.DownResponse(problem.Id.ToString(), response.Id.ToString(), memberVoting);

            // Reload to check if response note has changed.
            fromDb = _problemCollection.AsQueryable().Where(p => p.Id == problem.Id).First();
            var responseChanged = fromDb.Responses.First();

            Assert.That(responseChanged.Note, Is.EqualTo(response.Note - 1));
        }

        [Test]
        public void NoteCantBeDoneByResponseSubmitter()
        {
            var memberCreatingResponse = _session.Single<Member>().Get();
            _conn.Collection<Member>(CollectionNames.Member).Save(memberCreatingResponse);

            var problem = _problemService.CreateProblem("Problem 3244", "Some description", memberCreatingResponse);
            _problemService.AddResponse(problem.Id.ToString(), "Some solution", memberCreatingResponse);

            // Get the response from db
            var fromDb = _problemCollection.AsQueryable().Where(p => p.Id == problem.Id).First();
            var response = fromDb.Responses.First();

            Assert.That(
                () => _problemService.DownResponse(problem.Id.ToString(), response.Id.ToString(), memberCreatingResponse),
                Throws.Exception);

            Assert.That(
                () => _problemService.UpResponse(problem.Id.ToString(), response.Id.ToString(), memberCreatingResponse),
                Throws.Exception);
        }

        [Test]
        public void CloseProblem()
        {
            var memberCreatingResponse = _session.Single<Member>().Get();
            _conn.Collection<Member>(CollectionNames.Member).Save(memberCreatingResponse);

            var problem = _problemService.CreateProblem("Problem 3244", "Some description", memberCreatingResponse);
            _problemService.CloseProblem(problem.Id.ToString(), memberCreatingResponse);

            var fromDb = _problemCollection.AsQueryable().Where(x => x.Id == problem.Id).First();
            Assert.That(fromDb.CurrentSolution, Is.Null); // No solution was found.
            Assert.That(fromDb.ClosureDate, Is.Not.Null);
        }

        [Test]
        public void CantCloseProblemWhenMemberIsNotProblemSubmitter()
        {
            var memberCreatingProblem = _session.Single<Member>().Get();
            var anotherMember = _session.Single<Member>().Get();
            _conn.Collection<Member>(CollectionNames.Member).Save(memberCreatingProblem);

            var problem = _problemService.CreateProblem("Problem 3244", "Some description", memberCreatingProblem);

            Assert.That(
                () => _problemService.CloseProblem(problem.Id.ToString(), anotherMember),
                Throws.Exception);
        }

        [Test]
        public void ValidateAResponseAsSolution()
        {
            var memberCreatingProblem = _session.Single<Member>().Get();
            var memberCreatingResponse = _session.Single<Member>().Get();
            _conn.Collection<Member>(CollectionNames.Member).Save(memberCreatingProblem);
            _conn.Collection<Member>(CollectionNames.Member).Save(memberCreatingResponse);

            var problem = _problemService.CreateProblem("Problem 3244", "Some description", memberCreatingProblem);
            _problemService.AddResponse(problem.Id.ToString(), "Some solution", memberCreatingResponse);

            // Get the response from db
            var fromDb = _problemCollection.AsQueryable().Where(p => p.Id == problem.Id).First();
            var response = fromDb.Responses.First();

            _problemService.ValidateAsASolution(problem.Id.ToString(), memberCreatingProblem, response);

            // Reload problem
            fromDb = _problemCollection.AsQueryable().Where(p => p.Id == problem.Id).First();

            Assert.That(fromDb.CurrentSolution, Is.Not.Null);
            Assert.That(fromDb.CurrentSolution.Id, Is.EqualTo(response.Id));
            Assert.That(fromDb.Responses.First().Note, Is.EqualTo(response.Note + 1));
        }

        [Test]
        public void CantValidateSolutionWhenMemberIsNotProblemSubmitter()
        {
            var memberCreatingProblem = _session.Single<Member>().Get();
            var memberCreatingResponse = _session.Single<Member>().Get();
            _conn.Collection<Member>(CollectionNames.Member).Save(memberCreatingProblem);
            _conn.Collection<Member>(CollectionNames.Member).Save(memberCreatingResponse);

            var problem = _problemService.CreateProblem("Problem 3244", "Some description", memberCreatingProblem);
            _problemService.AddResponse(problem.Id.ToString(), "Some solution", memberCreatingResponse);

            // Get the response from db
            var fromDb = _problemCollection.AsQueryable().Where(p => p.Id == problem.Id).First();
            var response = fromDb.Responses.First();

            // Validate with wrong member
            Assert.That(
                () => _problemService.ValidateAsASolution(problem.Id.ToString(), memberCreatingResponse, response),
                Throws.Exception);
        }
    }
}
