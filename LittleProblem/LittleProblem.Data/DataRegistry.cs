using StructureMap.Configuration.DSL;
using LittleProblem.Data.Services;

namespace LittleProblem.Data
{
    public class DataRegistry : Registry
    {
        public DataRegistry()
        {
            For<IMembershipService>().Singleton().Use<MembershipService>();
        }
    }
}
