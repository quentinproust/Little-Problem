using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoPoco;
using AutoPoco.DataSources;
using AutoPoco.Engine;
using FakeItEasy;
using LittleProblem.Data.Model;
using LittleProblem.Data.Repository;
using LittleProblem.Test.Common;
using LittleProblem.Web.Controllers;
using LittleProblem.Web.Models;
using NUnit.Framework;

namespace LittleProblem.WebTest
{
    [TestFixture]
    public class HomeControllerTest
    {
        private readonly IGenerationSession _session;

        public HomeControllerTest()
        {
            // Perform factory set up (once for entire test run)
            IGenerationSessionFactory factory = AutoPocoContainer.Configure(x =>
            {
                x.Conventions(c => c.UseDefaultConventions());
                x.AddFromAssemblyContainingType<Problem>();

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

            _session = factory.CreateSession();
        }

        [Test]
        public void IndexDisplayByDefaultTheFirstsProblems()
        {
            var problems = _session.List<Problem>(10).Get();

            var problemRepository = A.Fake<IProblemRepository>();
            var homeController = new HomeController(problemRepository);

            A.CallTo(() => problemRepository.All(0)).Returns(problems);
            A.CallTo(() => problemRepository.Count()).Returns(120);

            var result = homeController.Index() as ViewResult;
            var problemsResult = result.ViewData.Model as ProblemListModel;

            Assert.That(result, Is.Not.Null);
            Assert.That(problemsResult.NbProblemsTotal, Is.EqualTo(120));
            Assert.That(problemsResult.Problems.Count(), Is.EqualTo(10));
            A.CallTo(() => problemRepository.All(0)).MustHaveHappened();
        }

        [Test]
        public void IndexDisplayNoProblemWhenNoProblemInRepo()
        {
            var problemRepository = A.Fake<IProblemRepository>();
            var homeController = new HomeController(problemRepository);

            A.CallTo(() => problemRepository.All(0)).Returns(new List<Problem>());
            A.CallTo(() => problemRepository.Count()).Returns(0);

            var result = homeController.Index() as ViewResult;
            var problemsResult = result.ViewData.Model as ProblemListModel;

            Assert.That(result, Is.Not.Null);
            Assert.That(problemsResult.NbProblemsTotal, Is.EqualTo(0));
            Assert.That(problemsResult.Problems.Count(), Is.EqualTo(0));
            A.CallTo(() => problemRepository.All(0)).MustHaveHappened();
        }

        [Test]
        public void IndexShow2ndPage()
        {
            var problems = _session.List<Problem>(5).Get();

            var problemRepository = A.Fake<IProblemRepository>();
            var homeController = new HomeController(problemRepository);

            A.CallTo(() => problemRepository.All(1)).Returns(problems);
            A.CallTo(() => problemRepository.Count()).Returns(15);

            var result = homeController.Index(1) as ViewResult;
            var problemsResult = result.ViewData.Model as ProblemListModel;

            Assert.That(result, Is.Not.Null);
            Assert.That(problemsResult.NbProblemsTotal, Is.EqualTo(15));
            Assert.That(problemsResult.Problems.Count(), Is.EqualTo(5));
            A.CallTo(() => problemRepository.All(1)).MustHaveHappened();
        }

        [Test]
        public void IndexShowErrorWhenPageNumberIsNegative()
        {
            var homeController = new HomeController(null);
            Assert.That(() => homeController.Index(-1), Throws.TypeOf<HttpException>());
        }
    }
}
