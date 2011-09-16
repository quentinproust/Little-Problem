using System.Web;
using System.Web.Mvc;
using LittleProblem.Web.Extension.Session;
using NLog;
using StructureMap;

namespace LittleProblem.Web.Extension.OpenId
{
    public class OpenIdAuthorizeAttribute : AuthorizeAttribute
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var sessionRegistry = ObjectFactory.GetInstance<ISessionRegistry>();
            return sessionRegistry.IsConnected();
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            Logger.Info("An anonymous user tried to access a restricted ressource. He will be redirected to Login page.");
            filterContext.HttpContext.Response.Redirect("/Account/Login");
        }
    }
}