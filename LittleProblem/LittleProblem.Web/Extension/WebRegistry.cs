using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LittleProblem.Web.Extension.Session;
using StructureMap.Configuration.DSL;

namespace LittleProblem.Web.Extension
{
    public class WebRegistry : Registry
    {
        public WebRegistry()
        {
            For<ISessionRegistry>().HttpContextScoped().Use<SessionRegistry>();
        }

    }
}