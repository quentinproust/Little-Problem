using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using DotNetOpenAuth.Messaging;
using DotNetOpenAuth.OpenId;
using DotNetOpenAuth.OpenId.Extensions.SimpleRegistration;
using DotNetOpenAuth.OpenId.RelyingParty;
using LittleProblem.Data;
using LittleProblem.Data.Model;
using LittleProblem.Data.Services;
using LittleProblem.Web.Models;

namespace LittleProblem.Web.Controllers
{
    [HandleError]
    public class AccountController : Controller
    {
        private readonly IMembershipService _membershipService;
        private readonly IMemberRepository _memberRepository;

        public AccountController(
            IMembershipService membershipService,
            IMemberRepository memberRepository)
        {
            _membershipService = membershipService;
            _memberRepository = memberRepository;
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
                        SuccessfulConnection(response);
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
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [OpenIdAuthorize]
        [HttpGet]
        public ActionResult Profile()
        {
            var member = _memberRepository.GetMember((string) Session["openId"]);
            if (member == null)
            {
                ViewData["Error"] = "This user does not exist.";
                return View();
            }

            return View(new ProfileModel
                            {
                                Email = member.Email ?? "", 
                                UserName = member.UserName
                            });
        }

        [OpenIdAuthorize]
        [HttpPost]
        public ActionResult Profile(ProfileModel model)
        {
            var member = new Member{
                Email = model.Email,
                UserName = model.UserName,
                OpenId = (string) Session["openId"]};
            _membershipService.EditMemberProfile(member);
            SaveSessionInfo(member.UserName, member.OpenId);

            return View(model);
        }

        private void SuccessfulConnection(IAuthenticationResponse response)
        {
            ClaimsResponse fetch = response.GetExtension(typeof(ClaimsResponse)) as ClaimsResponse;
            string email = null;
            if (fetch != null)
            {
                email = fetch.Email;
            }
            Member connectedMember = email == null 
                                         ? _membershipService.LogIn(response.ClaimedIdentifier.ToString()) 
                                         : _membershipService.LogIn(response.ClaimedIdentifier.ToString(), email);

            SaveSessionInfo(connectedMember.UserName, response.ClaimedIdentifier.ToString());
        }

        private void SaveSessionInfo(string userName, string openId)
        {
            Session.Add("username", userName);
            Session.Add("openid", openId);
        }
    }
}
