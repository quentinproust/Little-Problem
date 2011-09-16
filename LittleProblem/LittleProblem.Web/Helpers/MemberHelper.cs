using LittleProblem.Data.Model;
using LittleProblem.Web.Extension.Session;
using StructureMap;

namespace LittleProblem.Web.Helpers
{
    public static class MemberHelper
    {
        public static bool IsCurrentMember(this Member member)
        {
            var sessionRegistry = ObjectFactory.GetInstance<ISessionRegistry>();
            
            if (sessionRegistry.IsConnected())
            {
                var memberInformations = sessionRegistry.MemberInformations;
                return memberInformations.OpenId != null 
                    && memberInformations.OpenId.Equals(member.OpenId);
            }
            return false;
        }
    }
}