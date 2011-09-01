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
        private MemberInformations _memberInformations;
        public MemberInformations MemberInformations
        {
            get { return _memberInformations ?? (_memberInformations = new MemberInformations(Session)); }
            set { _memberInformations = value; }
        }

    }
}