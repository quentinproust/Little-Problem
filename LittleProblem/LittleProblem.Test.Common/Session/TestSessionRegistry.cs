using LittleProblem.Web.Extension.Session;

namespace LittleProblem.Test.Common.Session
{
    public class TestSessionRegistry : SessionRegistry
    {
        public TestSessionRegistry() : base(new SessionWrapper())
        {
        }
    }
}
