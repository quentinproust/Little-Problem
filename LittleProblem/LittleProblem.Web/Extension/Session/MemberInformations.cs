using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;

namespace LittleProblem.Web.Extension.Session
{
    public class MemberInformations : SessionObject
    {
        public MemberInformations()
        {
        }

        public MemberInformations(HttpSessionStateBase session) : base(session)
        {
        }

        public string OpenId
        {
            get { return Session["openid"] as string; }
            set { Session["openid"] = value; }
        }

        public string UserName
        {
            get { return Session["username"] as string; }
            set { Session["username"] = value; }
        }

    }
}