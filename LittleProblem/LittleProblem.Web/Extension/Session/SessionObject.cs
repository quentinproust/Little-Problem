using System.Web;

namespace LittleProblem.Web.Extension.Session
{
    /// <summary>
    /// Session object to initialize session property.
    /// </summary>
    public abstract class SessionObject
    {
        protected readonly HttpSessionStateBase Session;

        /// <summary>
        /// Initialize session from the current session in httpcontext.
        /// </summary>
        protected SessionObject(HttpSessionStateBase session)
        {
            Session = session;
        }
    }
}