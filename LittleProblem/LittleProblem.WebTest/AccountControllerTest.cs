using System.Web.Mvc;
using DotNetOpenAuth.OpenId.RelyingParty;
using FakeItEasy;
using LittleProblem.Data.Model;
using LittleProblem.Data.Repository;
using LittleProblem.Test.Common.Helpers;
using LittleProblem.Test.Common.Initializers;
using LittleProblem.Web.Extension.OpenId;
using LittleProblem.Web.Models;
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
            var accountController = ControllerLocator.GetAccountControllerForConnectedUser(memberRepository);

            A.CallTo(() => memberRepository.Get(member.OpenId)).Returns(member);
            
            var result = accountController.Profile() as ViewResult;
            var memberResult = result.ViewData.Model as ProfileModel;

            Assert.That(result, Is.Not.Null);
            Assert.That(memberResult.Email, Is.EqualTo(member.Email));
            Assert.That(memberResult.UserName, Is.EqualTo(member.UserName));
            Assert.That(memberResult.Note, Is.EqualTo(new NoteModel(member.Note)));
            
            A.CallTo(() => memberRepository.Get(member.OpenId)).MustHaveHappened();
        }

        [Test]
        public void LoadMemberProfileWithNoteWithoutEmail()
        {
            var member = new Member
            {
                Email = null, // No email was given by member
                OpenId = ConnectionHelper.OpenId,
                UserName = "Jack Test",
                Note = 2345
            };

            var memberRepository = A.Fake<IMemberRepository>();
            var accountController = ControllerLocator.GetAccountControllerForConnectedUser(memberRepository);

            A.CallTo(() => memberRepository.Get(member.OpenId)).Returns(member);

            var result = accountController.Profile() as ViewResult;
            var memberResult = result.ViewData.Model as ProfileModel;

            Assert.That(result, Is.Not.Null);
            Assert.That(memberResult.Email, Is.EqualTo(string.Empty));

            A.CallTo(() => memberRepository.Get(member.OpenId)).MustHaveHappened();
        }

        [Test]
        public void LogInFailedWhenMemberCancelRelyingPartyAuthentication()
        {
            var canceledResponse = A.Fake<IAuthenticationResponse>();
            var relyingParty = A.Fake<IAccountRelyingParty>();

            var accountController = ControllerLocator.GetAccountControllerForLoginTest(relyingParty);

            A.CallTo(() => canceledResponse.Status).Returns(AuthenticationStatus.Canceled);
            A.CallTo(() => relyingParty.GetResponse()).Returns(canceledResponse);

            var result = accountController.LogIn() as ViewResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Error(), Is.Not.Null.And.Not.EqualTo(string.Empty));
            Assert.That(result.Error(), Is.EqualTo(Resources.Errors.Account.LogInCancelInRelyingParty));
            
            A.CallTo(() => relyingParty.GetResponse()).MustHaveHappened();
            A.CallTo(() => canceledResponse.Status).MustHaveHappened();
        }

        [Test]
        public void LogInFailedWhenMemberFailRelyingPartyAuthentication()
        {
            var failedResponse = A.Fake<IAuthenticationResponse>();
            var relyingParty = A.Fake<IAccountRelyingParty>();

            var accountController = ControllerLocator.GetAccountControllerForLoginTest(relyingParty);

            A.CallTo(() => failedResponse.Status).Returns(AuthenticationStatus.Failed);
            A.CallTo(() => relyingParty.GetResponse()).Returns(failedResponse);

            var result = accountController.LogIn() as ViewResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Error(), Is.Not.Null.And.Not.EqualTo(string.Empty));
            Assert.That(result.Error(), Is.EqualTo(Resources.Errors.Account.LogInFailedWithProvidedIdentifier));

            A.CallTo(() => relyingParty.GetResponse()).MustHaveHappened();
            A.CallTo(() => failedResponse.Status).MustHaveHappened();
        }

        [Test]
        public void LogInMemberWhenRelyingPartyAuthenticationIsSuccessfull()
        {
            var successResponse = A.Fake<IAuthenticationResponse>();
            var relyingParty = A.Fake<IAccountRelyingParty>();

            var accountController = ControllerLocator.GetAccountControllerForLoginTest(relyingParty);

            A.CallTo(() => successResponse.Status).Returns(AuthenticationStatus.Authenticated);
            A.CallTo(() => relyingParty.GetResponse()).Returns(successResponse);

            var result = accountController.LogIn() as RedirectToRouteResult;
            
            Assert.That(result, Is.Not.Null);
            Assert.That(result.RouteValues["action"], Is.EqualTo("Index"));
            Assert.That(result.RouteValues["controller"], Is.EqualTo("Home"));

            A.CallTo(() => relyingParty.GetResponse()).MustHaveHappened();
            A.CallTo(() => successResponse.Status).MustHaveHappened();
            A.CallTo(() => relyingParty.LogInMember(successResponse)).MustHaveHappened();
        }

        [Test]
        public void LogInFailWithInvalidIndentifier()
        {
            var loginModel = new LogInModel {OpenId = ConnectionHelper.OpenId};
            var relyingParty = A.Fake<IAccountRelyingParty>();

            var accountController = ControllerLocator.GetAccountControllerForLoginTest(relyingParty);

            A.CallTo(() => relyingParty.IsValidIdentifier(ConnectionHelper.OpenId)).Returns(false);

            var result = accountController.LogIn(loginModel) as ViewResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Error("loginIdentifier"), Is.Not.Null);
            Assert.That(result.Error("loginIdentifier"), Is.EqualTo(Resources.Errors.Account.InvalidIndentifier));

            A.CallTo(() => relyingParty.IsValidIdentifier(ConnectionHelper.OpenId)).MustHaveHappened();
        }

        [Test]
        public void TryLogInByRedirectingUserToRelyingParty()
        {
            var loginModel = new LogInModel { OpenId = ConnectionHelper.OpenId };
            // We don't care what the result is. We just want to check we get back the same object
            var redirectResult = new EmptyResult(); 
            var relyingParty = A.Fake<IAccountRelyingParty>();

            var accountController = ControllerLocator.GetAccountControllerForLoginTest(relyingParty);

            A.CallTo(() => relyingParty.IsValidIdentifier(ConnectionHelper.OpenId)).Returns(true);
            A.CallTo(() => relyingParty.CreateRequest(ConnectionHelper.OpenId)).Returns(redirectResult);

            var result = accountController.LogIn(loginModel);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.SameAs(redirectResult));

            A.CallTo(() => relyingParty.IsValidIdentifier(ConnectionHelper.OpenId)).MustHaveHappened();
            A.CallTo(() => relyingParty.CreateRequest(ConnectionHelper.OpenId)).MustHaveHappened();
        }

        [Test]
        public void LogUserOut()
        {
            var relyingParty = A.Fake<IAccountRelyingParty>();

            var accountController = ControllerLocator.GetAccountControllerForLoginTest(relyingParty);

            A.CallTo(() => relyingParty.LogOut());

            var result = accountController.LogOut();

            Assert.That(result, Is.Not.Null);

            A.CallTo(() => relyingParty.LogOut()).MustHaveHappened();
        }
    }
}
