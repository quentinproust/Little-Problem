using LittleProblem.Web.Extension.OpenId;
using LittleProblem.Web.Extension.Session;
using StructureMap.Configuration.DSL;

namespace LittleProblem.Web.Extension
{
    public class WebRegistry : Registry
    {
        public WebRegistry()
        {
            For<ISessionRegistry>().HttpContextScoped().Use<SessionRegistry>();
#if DEBUG
            For<IAccountRelyingParty>().HttpContextScoped().Use<FakeAccountRelyingParty>();
#else
            For<IAccountRelyingParty>().HttpContextScoped().Use<AccountRelyingParty>();
#endif

        }

    }
}