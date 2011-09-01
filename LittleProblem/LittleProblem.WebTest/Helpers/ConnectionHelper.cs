using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace LittleProblem.WebTest.Helpers
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
        public static void ConnectUser(this Controller controller)
        {
            controller.Session["openId"] = OpenId;
        }

        /// <summary>
        /// Connect user with given open id
        /// </summary>
        /// <param name="controller">Controller with session</param>
        /// <param name="openId">Open Id</param>
        /// <remarks>Just add informations about connected user in session</remarks>
        public static void ConnectUser(this Controller controller, string openId)
        {
            controller.Session["openId"] = openId;
        }
    }
}
