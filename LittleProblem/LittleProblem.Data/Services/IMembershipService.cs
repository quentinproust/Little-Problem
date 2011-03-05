using LittleProblem.Data.Model;

namespace LittleProblem.Data.Services
{
    /// <summary>
    /// Deals with member control in database.
    /// </summary>
    public interface IMembershipService
    {
        /// <summary>
        /// Log a user in.
        /// </summary>
        /// <param name="openId">OpenId Identifier</param>
        Member LogIn(string openId);

        /// <summary>
        /// Log a user in.
        /// </summary>
        /// <param name="openId">OpenId Identifier</param>
        /// <param name="email">Email</param>
        Member LogIn(string openId, string email);

        /// <summary>
        /// Will create the member profile when he log for the first time.
        /// </summary>
        /// <param name="openId">OpenId Identifier</param>
        /// <param name="email">Email</param>
        Member CreateOnFirstLogIn(string openId, string email);
    }
}
