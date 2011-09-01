using System.Web.Mvc;
using FakeItEasy;
using LittleProblem.Data.Model;
using LittleProblem.Data.Repository;
using LittleProblem.Test.Common;
using LittleProblem.Web.Controllers;
using LittleProblem.Web.Models;
using LittleProblem.WebTest.Helpers;
using MongoDB.Bson;
using NUnit.Framework;

namespace LittleProblem.WebTest
{
    [TestFixture]
    class AccountControllerTest
    {

        [Test]
        public void LoadMemberProfileWithNote()
        {
            var member = new Member
                             {
                                 Email = "test@email.com",
                                 OpenId = ConnectionHelper.OpenId,
                                 UserName = "Jack Test",
                                 Note = 2345
                             };

            var memberRepository = A.Fake<IMemberRepository>();
            var accountController = new AccountController(null, memberRepository, null);
            accountController.InjectFakeContext();
            accountController.ConnectUser();

            A.CallTo(() => memberRepository.Get(member.OpenId)).Returns(member);
            
            var result = accountController.Profile() as ViewResult;
            var memberResult = result.ViewData.Model as ProfileModel;

            Assert.That(result, Is.Not.Null);
            Assert.That(memberResult.Email, Is.EqualTo(member.Email));
            Assert.That(memberResult.UserName, Is.EqualTo(member.UserName));
            Assert.That(memberResult.Note, Is.EqualTo(new NoteModel(member.Note)));
            
            A.CallTo(() => memberRepository.Get(member.OpenId)).MustHaveHappened();
        }

    }
}
