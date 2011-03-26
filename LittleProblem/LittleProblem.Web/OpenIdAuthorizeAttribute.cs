using System.Web;
using System.Web.Mvc;
using NLog;

namespace LittleProblem.Web
{
    public class OpenIdAuthorizeAttribute : AuthorizeAttribute
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.Session != null) return httpContext.Session["openid"] != null;
            return false;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            logger.Info("An anonymous user tried to access a restricted ressource. He will be redirected to Login page.");
            filterContext.HttpContext.Response.Redirect("/Account/Login");
        }
    }
}