using LittleProblem.Web.Extension;

namespace LittleProblem.Test.Common.Helpers
{
    public static class ConnectionHelper
    {
        /// <summary>
        /// Default open id for test
        /// </summary>
        public const string OpenId = "http://usertest.openid.com/";

        /// <summary>
        /// Connect user with default open id
        /// </summary>
        /// <param name="controller">Controller with session</param>
        /// <remarks>Just add informations about connected user in session</remarks>
        public static void ConnectUser(this BaseController controller)
        {
            controller.MemberInformations.OpenId = OpenId;
        }
    }
}
