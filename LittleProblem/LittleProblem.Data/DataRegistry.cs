using LittleProblem.Data.Repository;
using LittleProblem.Data.Server;
using StructureMap.Configuration.DSL;
using LittleProblem.Data.Services;

namespace LittleProblem.Data
{
    public class DataRegistry : Registry
    {
        public DataRegistry()
        {
            For<IConnexion>().HttpContextScoped().Use(x => new DbConnexion());

            For<IMembershipService>().Use<MembershipService>();

            For<IMemberRepository>().Use<MemberRepository>();
        }
    }
}
