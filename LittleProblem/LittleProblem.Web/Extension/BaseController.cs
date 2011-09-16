using System.Web.Mvc;
using LittleProblem.Web.Extension.Session;

namespace LittleProblem.Web.Extension
{
    public class BaseController : Controller
    {
        private readonly ISessionRegistry _sessionRegistry;

        protected BaseController(ISessionRegistry sessionRegistry)
        {
            _sessionRegistry = sessionRegistry;
        }

        public MemberInformations MemberInformations
        {
            get { return _sessionRegistry.MemberInformations; }
        }

    }
}