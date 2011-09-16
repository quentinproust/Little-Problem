using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LittleProblem.Web.Extension.Session
{
    public interface ISessionRegistry
    {
        void CleanSession();
        MemberInformations MemberInformations { get; }
    }
}
