using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LittleProblem.Web.Extension.Session
{
    public class SessionRegistry : ISessionRegistry
    {
        /// <summary>
        /// Session property.
        /// This is the session used in mvc.
        /// </summary>
        protected readonly HttpSessionStateBase _session;

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