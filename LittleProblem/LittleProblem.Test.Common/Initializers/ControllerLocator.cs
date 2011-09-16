using LittleProblem.Data.Repository;
using LittleProblem.Data.Services;
using LittleProblem.Test.Common.Helpers;
using LittleProblem.Test.Common.Session;
using LittleProblem.Web.Controllers;
using LittleProblem.Web.Extension;
using LittleProblem.Web.Extension.OpenId;

namespace LittleProblem.Test.Common.Initializers
{
    public static class ControllerLocator
    {
        public static AccountController GetAccountControllerForConnectedUser(IMemberRepository memberRepository)
        {
            var accountController = new AccountController(null, memberRepository, null, new TestSessionRegistry());
            ConnectController(accountController);

            return accountController;
        }

        public static AccountController GetAccountControllerForLoginTest(IAccountRelyingParty relyingParty)
        {
            var accountController = new AccountController(null, null, relyingParty, new TestSessionRegistry());
            accountController.InjectFakeContext();

            return accountController;
        }

        public static ProblemController GetProblemController(
            IMemberRepository memberRepository, IProblemService problemService, IProblemRepository problemRepository)
        {
            var problemController = new ProblemController(memberRepository, problemService, problemRepository, new TestSessionRegistry());
            problemController.InjectFakeContext();
            problemController.ConnectUser();

            return problemController;
        }

        private static void ConnectController(BaseController controller)
        {
            controller.InjectFakeContext();
            controller.ConnectUser();
        }
    }
}
