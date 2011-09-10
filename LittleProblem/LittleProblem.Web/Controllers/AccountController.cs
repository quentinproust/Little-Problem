using System.Web.Mvc;
using DotNetOpenAuth.OpenId.RelyingParty;
using LittleProblem.Data.Model;
using LittleProblem.Data.Repository;
using LittleProblem.Data.Services;
using LittleProblem.Web.Extension;
using LittleProblem.Web.Extension.OpenId;
using LittleProblem.Web.Models;

namespace LittleProblem.Web.Controllers
{
    [HandleError]
    public class AccountController : BaseController
    {
        private readonly IMembershipService _membershipService;
        private readonly IMemberRepository _memberRepository;
        private readonly IAccountRelyingParty _relyingParty;

        public AccountController(
            IMembershipService membershipService,
            IMemberRepository memberRepository,
            IAccountRelyingParty relyingParty)
        {
            _membershipService = membershipService;
            _memberRepository = memberRepository;
            _relyingParty = relyingParty;
        }

        public ActionResult LogIn()
        {
            var response = _relyingParty.GetResponse();

            if (response != null)
            {
                switch (response.Status)
                {
                    case AuthenticationStatus.Authenticated:
                        _relyingParty.LogInMember(response);
                        return RedirectToAction("Index", "Home");

                    case AuthenticationStatus.Canceled:
                        ModelState.AddModelError("",
                            Resources.Errors.Account.LogInCancelInRelyingParty);
                        break;

                    case AuthenticationStatus.Failed:
                        ModelState.AddModelError("",
                            Resources.Errors.Account.LogInFailedWithProvidedIdentifier);
                        break;
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(LogInModel model)
        {
            if (!_relyingParty.IsValidIdentifier(model.OpenId))
            {
                ModelState.AddModelError("loginIdentifier",
                            Resources.Errors.Account.InvalidIndentifier);
                return View();
            }

            return _relyingParty.CreateRequest(model.OpenId);
        }

        public ActionResult LogOut()
        {
            _relyingParty.LogOut();
            return RedirectToAction("Index", "Home");
        }

        [OpenIdAuthorize]
        [HttpGet]
        public ActionResult Profile()
        {
            var member = _memberRepository.Get(MemberInformations.OpenId);
            if (member == null)
            {
                ViewData["Error"] = "This user does not exist.";
                return View();
            }

            return View(new ProfileModel
                            {
                                Email = member.Email ?? "", 
                                UserName = member.UserName,
                                Note = new NoteModel(member.Note)
                            });
        }

        [OpenIdAuthorize]
        [HttpPost]
        public ActionResult Profile(ProfileModel model)
        {
            var member = new Member{
                Email = model.Email,
                UserName = model.UserName,
                OpenId = MemberInformations.OpenId};
            _membershipService.EditMemberProfile(member);

            MemberInformations.UserName = member.UserName;

            return View(model);
        }
    }
}
