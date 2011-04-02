using System.Web;
using System.Web.SessionState;
using LittleProblem.Data.Model;

namespace LittleProblem.Web.Helpers
{
    public static class MemberHelper
    {
        public static bool IsCurrentMember(this HttpSessionState sessionState, Member member)
        {
            if (sessionState != null)
            {
                var openId = sessionState["openid"];
                return openId != null && openId.Equals(member.OpenId);
            }
            return false;
        }

        public static bool IsCurrentMember(this HttpSessionStateBase sessionState, Member member)
        {
            if (sessionState != null)
            {
                var openId = sessionState["openid"];
                return openId != null && openId.Equals(member.OpenId);
            }
            return false;
        }
    }
}