using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LittleProblem.Data;
using LittleProblem.Data.Model;
using LittleProblem.Data.Repository;
using LittleProblem.Data.Services;
using LittleProblem.Web.Models;

namespace LittleProblem.Web.Controllers
{
    public class ProblemController : Controller
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IProblemService _problemService;
        private readonly IProblemRepository _problemRepository;
        
        public ProblemController(
            IMemberRepository memberRepository, 
            IProblemService problemService, 
            IProblemRepository problemRepository)
        {
            _memberRepository = memberRepository;
            _problemService = problemService;
            _problemRepository = problemRepository;
        }

        public ActionResult Details(String id)
        {
            var problem = _problemRepository.Get(id);
            if (problem == null)
            {
                ViewData["Error"] = "This problem was not found.";
            }
            return View(problem);
        }

        [OpenIdAuthorize]
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [OpenIdAuthorize]
        [HttpPost]
        public ActionResult Create(ProblemModel model)
        {
            var member = _memberRepository.GetMember((string) Session["openId"]);
            if (member == null)
            {
                ViewData["Error"] = "There is no known user.";
                return View();
            }
            Problem problem = _problemService.CreateProblem(model.Title, model.Text, member);
            return RedirectToAction("Details", new { id = problem.Id.ToString() });
        }

        [OpenIdAuthorize]
        [HttpPost]
        public ActionResult Answer(ResponseModel model)
        {
            var member = _memberRepository.GetMember((string)Session["openId"]);
            if (member == null)
            {
                ViewData["Error"] = "There is no known user.";
                return Redirect("/Login.aspx");
            }
            _problemService.AddResponse(model.ProblemId, model.Text, member);
            return RedirectToAction("Details", new { id = model.ProblemId });
        }

        [OpenIdAuthorize]
        [HttpPost]
        public ActionResult Close(ActionOnProblemModel model)
        {
            var member = _memberRepository.GetMember((string)Session["openId"]);
            if (member == null)
            {
                ViewData["Error"] = "There is no known user.";
                return Redirect("/Login.aspx");
            }
            return new RedirectResult("/");
        }

    }
}
