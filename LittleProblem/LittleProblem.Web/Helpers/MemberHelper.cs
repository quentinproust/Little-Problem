using System.Web;
using System.Web.SessionState;
using LittleProblem.Data.Model;
using LittleProblem.Web.Extension.Session;

namespace LittleProblem.Web.Helpers
{
    public static class MemberHelper
    {
        public static bool IsCurrentMember(this HttpSessionState sessionState, Member member)
        {
            return IsCurrentMember(new HttpSessionStateWrapper(sessionState), member);
        }

        public static bool IsCurrentMember(this HttpSessionStateBase sessionState, Member member)
        {
            if (sessionState != null)
            {
                var memberInformations = new MemberInformations(sessionState);
                return memberInformations.OpenId != null 
                    && memberInformations.OpenId.Equals(member.OpenId);
            }
            return false;
        }
    }
}