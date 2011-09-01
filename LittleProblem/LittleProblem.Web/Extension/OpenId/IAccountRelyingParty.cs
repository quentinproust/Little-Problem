using System.Web.Mvc;
using DotNetOpenAuth.OpenId.RelyingParty;

namespace LittleProblem.Web.Extension.OpenId
{
    /// <summary>
    /// Interface for connecting user through the relying party.
    /// </summary>
    public interface IAccountRelyingParty
    {

        /// <summary>
        /// Get response from the relying party to log in user.
        /// </summary>
        /// <returns>Authentication response from the relying party</returns>
        IAuthenticationResponse GetResponse();

        /// <summary>
        /// Check if the given identifier is valid.
        /// </summary>
        /// <param name="identifier">Identifier.</param>
        /// <returns>Is Valid</returns>
        bool IsValidIdentifier(string identifier);

        /// <summary>
        /// Create a mvc request to log user in the relying party account system.
        /// </summary>
        /// <param name="identifier">Identifier</param>
        /// <returns>Request</returns>
        ActionResult CreateRequest(string identifier);

        /// <summary>
        /// Log member in from response informations.
        /// </summary>
        /// <param name="response">A successfull response from the relying party</param>
        void LogInMember(IAuthenticationResponse response);

        /// <summary>
        /// Log user out
        /// </summary>
        void LogOut();

    }
}