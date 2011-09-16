using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LittleProblem.Web.Extension.Session;

namespace LittleProblem.Web.Extension
{
    public class BaseController : Controller
    {
        protected readonly ISessionRegistry SessionRegistry;

        protected BaseController(ISessionRegistry sessionRegistry)
        {
            SessionRegistry = sessionRegistry;
        }

        public MemberInformations MemberInformations
        {
            get { return SessionRegistry.MemberInformations; }
        }

    }
}