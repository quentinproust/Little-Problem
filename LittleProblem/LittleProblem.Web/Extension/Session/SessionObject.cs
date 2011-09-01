using System.Web;

namespace LittleProblem.Web.Extension.Session
{
    /// <summary>
    /// Session object to initialize session property.
    /// </summary>
    public class SessionObject
    {

        /// <summary>
        /// Session property.
        /// This is the session used in mvc.
        /// </summary>
        public HttpSessionStateBase Session { get; private set; }

        /// <summary>
        /// Initialize session from the current session in httpcontext.
        /// </summary>
        public SessionObject()
        {
            Session = new HttpContextWrapper(HttpContext.Current).Session;
        }

        /// <summary>
        /// Initialize session from object given from user.
        /// </summary>
        /// <param name="session">session object</param>
        public SessionObject(HttpSessionStateBase session)
        {
            Session = session;
        }

        public void Clear()
        {
            Session.Clear();
        }
    }
}