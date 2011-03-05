using System.Web.Mvc;
using DotNetOpenAuth.Messaging;
using DotNetOpenAuth.OpenId;
using DotNetOpenAuth.OpenId.Extensions.SimpleRegistration;
using DotNetOpenAuth.OpenId.RelyingParty;
using LittleProblem.Data.Services;
using LittleProblem.Web.Models;

namespace LittleProblem.Web.Controllers
{
    [HandleError]
    public class AccountController : Controller
    {
        private readonly IMembershipService _membershipService;

        public AccountController(IMembershipService membershipService)
        {
            _membershipService = membershipService;
        }

        public ActionResult LogIn()
        {
            var openid = new OpenIdRelyingParty();
            IAuthenticationResponse response = openid.GetResponse();

            if (response != null)
            {
                switch (response.Status)
                {
                    case AuthenticationStatus.Authenticated:
                        // TODO : register user for its first login                       
                        Session.Add("openid", response.ClaimedIdentifier.ToString());
                        
                        ClaimsResponse fetch = response.GetExtension(typeof(ClaimsResponse)) as ClaimsResponse;
                        string email = null;
                        if (fetch != null)
                        {
                             email = fetch.Email;
                        }
                        if (email == null)
                        {
                            _membershipService.LogIn(response.ClaimedIdentifier.ToString());
                        }
                        else
                        {
                            _membershipService.LogIn(response.ClaimedIdentifier.ToString(), email);
                        }
                        

                        return RedirectToAction("Index", "Home");

                    case AuthenticationStatus.Canceled:
                        ModelState.AddModelError("",
                            "Login was cancelled at the provider");
                        break;

                    case AuthenticationStatus.Failed:
                        ModelState.AddModelError("",
                            "Login failed using the provided OpenID identifier");
                        break;
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(LogInModel model)
        {
            if (!Identifier.IsValid(model.OpenId))
            {
                ModelState.AddModelError("loginIdentifier",
                            "The specified login identifier is invalid");
                return View();
            }
            else
            {
                var openid = new OpenIdRelyingParty();
                IAuthenticationRequest request = openid.CreateRequest(
                    Identifier.Parse(model.OpenId));

                // Require email. We can latter use it for Gravatar 
                // or for retriving forgotten open id.
                request.AddExtension(new ClaimsRequest { Email = DemandLevel.Require });
                return request.RedirectingResponse.AsActionResult();
            }
        }

        public ActionResult LogOut()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
