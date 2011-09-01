using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.Messaging;
using DotNetOpenAuth.OpenId;
using DotNetOpenAuth.OpenId.Extensions.SimpleRegistration;
using DotNetOpenAuth.OpenId.RelyingParty;
using LittleProblem.Data.Model;
using LittleProblem.Data.Services;
using LittleProblem.Web.Extension.Session;

namespace LittleProblem.Web.Extension.OpenId
{
    public class AccountRelyingParty
    {
        private readonly IMembershipService _membershipService;
        private readonly OpenIdRelyingParty _openid;
        private readonly MemberInformations _memberInformations;

        public AccountRelyingParty(IMembershipService membershipService)
        {
            _membershipService = membershipService;
            _openid = new OpenIdRelyingParty();
            _memberInformations = new MemberInformations();
        }

        /// <summary>
        /// Get response from the relying party to log in user.
        /// </summary>
        /// <returns>Authentication response from the relying party</returns>
        IAuthenticationResponse GetResponse()
        {
            return _openid.GetResponse();
        }

        /// <summary>
        /// Check if the given identifier is valid.
        /// </summary>
        /// <param name="identifier">Identifier.</param>
        /// <returns>Is Valid</returns>
        bool IsValidIdentifier(string identifier)
        {
            return Identifier.IsValid(identifier);
        }

        /// <summary>
        /// Create a mvc request to log user in the relying party account system.
        /// </summary>
        /// <param name="identifier">Identifier</param>
        /// <returns>Request</returns>
        ActionResult CreateRequest(string identifier)
        {
            IAuthenticationRequest request = _openid.CreateRequest(
                Identifier.Parse(identifier));

            // Require email. We can latter use it for Gravatar 
            // or for retriving forgotten open id.
            request.AddExtension(new ClaimsRequest { Email = DemandLevel.Require });
            return request.RedirectingResponse.AsActionResult();
        }

        /// <summary>
        /// Log member in from response informations.
        /// </summary>
        /// <param name="response">A successfull response from the relying party</param>
        void LogInMember(IAuthenticationResponse response)
        {
            var fetch = response.GetExtension(typeof(ClaimsResponse)) as ClaimsResponse;
            string email = null;
            if (fetch != null)
            {
                email = fetch.Email;
            }
            Member connectedMember = email == null 
                                         ? _membershipService.LogIn(response.ClaimedIdentifier.ToString()) 
                                         : _membershipService.LogIn(response.ClaimedIdentifier.ToString(), email);

            _memberInformations.UserName = connectedMember.UserName;
            _memberInformations.OpenId = response.ClaimedIdentifier.ToString();
        }

        public void LogOut()
        {
            _memberInformations.Clear();
            FormsAuthentication.SignOut();
        }
    }
}