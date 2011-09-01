using System.Web.Mvc;
using AutoPoco;
using AutoPoco.DataSources;
using AutoPoco.Engine;
using FakeItEasy;
using LittleProblem.Data.Model;
using LittleProblem.Data.Repository;
using LittleProblem.Data.Services;
using LittleProblem.Test.Common;
using LittleProblem.Web.Controllers;
using LittleProblem.Web.Helpers;
using LittleProblem.Web.Models;
using LittleProblem.WebTest.Helpers;
using NUnit.Framework;

namespace LittleProblem.WebTest
{
    [TestFixture]
    public class ProblemControllerTest
    {
        private readonly IGenerationSession _session;

        public ProblemControllerTest()
        {
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

        [Test]
        public void CreateProblem()
        {
            var problemService = A.Fake<IProblemService>();
            var problemRepository = A.Fake<IProblemRepository>();
            var memberRepository = A.Fake<IMemberRepository>();
            var problemController = new ProblemController(memberRepository,problemService, problemRepository);
            problemController.InjectFakeContext();
            problemController.ConnectUser();

            var problem = _session.Single<Problem>().Get();
            var member = _session.Single<Member>().Get();

            var problemModel = new ProblemModel {Title = problem.Title,Text = problem.Text};

            A.CallTo(() => memberRepository.Get(member.OpenId)).Returns(member);
            A.CallTo(() => problemService.CreateProblem(problem.Title, problem.Text.TransformLine(), member)).Returns(problem);

            var redirect = problemController.Create(problemModel) as RedirectToRouteResult;

            Assert.That(redirect, Is.Not.Null);
            Assert.That(redirect.RouteValues["action"], Is.EqualTo("Details"));
            Assert.That(redirect.RouteValues["id"], Is.EqualTo(problem.Id.ToString()));
        }
    }
}
