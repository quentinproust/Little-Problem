using AutoPoco;
using AutoPoco.DataSources;
using AutoPoco.Engine;
using LittleProblem.Common.Colllections;
using LittleProblem.Common.CustomException;
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
    public class ProblemRepositoryTest
    {
        private readonly IGenerationSession _session;
        private ProblemRepository _problemRepository;
        private MongoCollection<Problem> _problemCollection;
        private DbConnexion _conn;

        public ProblemRepositoryTest()
        {
            // Perform factory set up (once for entire test run)
            IGenerationSessionFactory factory = AutoPocoContainer.Configure(x =>
            {
                x.Conventions(c =>
                {
                    c.UseDefaultConventions();
                });
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

            _conn = new DbConnexion("Test_LittleProblem");
            _problemRepository = new ProblemRepository(_conn);
            _problemCollection = _conn.Collection<Problem>(CollectionNames.Problem);
        }

        [SetUp]
        public void SetUp()
        {
            _problemCollection.RemoveAll();
            _conn.Collection<Member>(CollectionNames.Member).RemoveAll();
        }

        [Test]
        public void LoadProblemWithId()
        {
            var members = _session.List<Member>(3).Get();
            _conn.Collection<Member>(CollectionNames.Member).InsertBatch(members);

            var responses = _session.List<Response>(5)
                .Random(3).Impose(x => x.UserId, members[0].Id)
                .Next(1).Impose(x => x.UserId, members[1].Id)
                .Next(1).Impose(x => x.UserId, members[2].Id)
                .All().Get();

            var problem = _session.Single<Problem>()
                .Impose(x => x.Responses, responses)
                .Impose(x => x.UserId, members[0].Id)
                .Get();

            _problemCollection.Save(problem);

            var fromDb = _problemRepository.Get(problem.Id.ToString());

            Assert.That(fromDb, Is.Not.Null);
            Assert.That(new ObjectId(fromDb.Id), Is.EqualTo(problem.Id));
            Assert.That(fromDb.Submitter.Id, Is.EqualTo(problem.UserId));
            Assert.That(fromDb.Responses.Count, Is.EqualTo(responses.Count));
        }

        [Test]
        public void ThrowExceptionWhenProblemNotFound()
        {
            Assert.That(
                () => _problemRepository.Get(ObjectId.GenerateNewId().ToString()), 
                Throws.TypeOf<EntityNotFoundException>());
        }

        [Test]
        public void LoadNoProblemWhenDbIsEmpty()
        {
            var problems = _problemRepository.All(0);
            Assert.That(problems, Is.Not.Null);
            Assert.That(problems.Count, Is.EqualTo(0));
        }

        [Test]
        public void Load3ProblemWhenDbOnlyHas3WhenStartingFromFirstProblem()
        {
            var members = _session.List<Member>(2).Get();
            _conn.Collection<Member>(CollectionNames.Member).InsertBatch(members);

            var generatedProblems = _session.List<Problem>(3)
                .First(1).Impose(x => x.UserId, members[0].Id)
                .Next(2).Impose(x => x.UserId, members[1].Id)
                .All().Get();

            _problemCollection.InsertBatch(generatedProblems);

            var problems = _problemRepository.All(0);
            Assert.That(problems, Is.Not.Null);
            Assert.That(problems.Count, Is.EqualTo(3));
        }

        /// <summary>
        /// When there is more problems in db than page size allows to display,
        /// we only get back #PageSize problems.
        /// </summary>
        [Test]
        public void LoadProblemWhenGreaterThanPageSizeWhenStartingFromFirstProblem()
        {
            var members = _session.List<Member>(2).Get();
            _conn.Collection<Member>(CollectionNames.Member).InsertBatch(members);

            var pageSize = _problemRepository.PageSize;
            var generatedProblems = _session.List<Problem>(pageSize +10)
                .Random(pageSize)
                    .Impose(x => x.UserId, members[0].Id)
                .Next(10)
                    .Impose(x => x.UserId, members[1].Id)
                .All().Get();

            _problemCollection.InsertBatch(generatedProblems);

            var problems = _problemRepository.All(0);
            Assert.That(problems, Is.Not.Null);
            Assert.That(problems.Count, Is.EqualTo(pageSize));
        }

        [Test]
        public void LoadProblemWhenGreaterThanPageSizeWhenStartingFromSecondPage()
        {
            var members = _session.List<Member>(2).Get();
            _conn.Collection<Member>(CollectionNames.Member).InsertBatch(members);

            var pageSize = _problemRepository.PageSize;
            var generatedProblems = _session.List<Problem>(pageSize + 3)
                .Random(pageSize)
                    .Impose(x => x.UserId, members[0].Id)
                .Next(10)
                    .Impose(x => x.UserId, members[1].Id)
                .All().Get();

            _problemCollection.InsertBatch(generatedProblems);

            var problems = _problemRepository.All(1); // Get second page
            Assert.That(problems, Is.Not.Null);
            Assert.That(problems.Count, Is.EqualTo(3));
        }

        [Test]
        public void GetNumberOfProblem()
        {
            var members = _session.List<Member>(2).Get();
            _conn.Collection<Member>(CollectionNames.Member).InsertBatch(members);

            var generatedProblems = _session.List<Problem>(20)
                .Random(10)
                    .Impose(x => x.UserId, members[0].Id)
                .Next(10)
                    .Impose(x => x.UserId, members[1].Id)
                .All().Get();

            _problemCollection.InsertBatch(generatedProblems);

            var count = _problemRepository.Count();
            Assert.That(count, Is.EqualTo(20));
        }
    }
}
