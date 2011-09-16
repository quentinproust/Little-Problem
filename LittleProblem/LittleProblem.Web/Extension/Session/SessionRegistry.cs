using System;
using System.Web;

namespace LittleProblem.Web.Extension.Session
{
    public class SessionRegistry : ISessionRegistry
    {
        /// <summary>
        /// Session property.
        /// This is the session used in mvc.
        /// </summary>
        private readonly HttpSessionStateBase _session;

        private readonly MemberInformations _memberInformations;

        public SessionRegistry()
            : this(new HttpContextWrapper(HttpContext.Current).Session)
        {
        }

        /// <summary>
        /// Initialize session registry.
        /// </summary>
        protected SessionRegistry(HttpSessionStateBase session)
        {
            _session = session;
            _memberInformations = new MemberInformations(_session);
        }

        public bool IsConnected()
        {
            return MemberInformations != null && !String.IsNullOrEmpty(MemberInformations.OpenId);
        }

        public void CleanSession()
        {
            _session.Clear();
        }

        public MemberInformations MemberInformations
        {
            get { return _memberInformations; }
        }
    }
}