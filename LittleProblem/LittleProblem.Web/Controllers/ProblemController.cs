﻿using System;
using System.Web.Mvc;
using LittleProblem.Data.Repository;
using LittleProblem.Data.Services;
using LittleProblem.Web.Extension;
using LittleProblem.Web.Extension.OpenId;
using LittleProblem.Web.Extension.Session;
using LittleProblem.Web.Helpers;
using LittleProblem.Web.Models;

namespace LittleProblem.Web.Controllers
{
    public class ProblemController : BaseController
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IProblemService _problemService;
        private readonly IProblemRepository _problemRepository;
        
        public ProblemController(
            IMemberRepository memberRepository, 
            IProblemService problemService, 
            IProblemRepository problemRepository,
            ISessionRegistry sessionRegistry) : base(sessionRegistry)
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
            var member = _memberRepository.Get(MemberInformations.OpenId);
            if (member == null)
            {
                ViewData["Error"] = "There is no known user.";
                return View();
            }
            var problem = _problemService.CreateProblem(
                model.Title, 
                model.Text.TransformLine(), 
                member);
            return RedirectToAction("Details", new { id = problem.Id.ToString() });
        }

        [OpenIdAuthorize]
        [HttpPost]
        public ActionResult Answer(ResponseModel model)
        {
            var member = _memberRepository.Get(MemberInformations.OpenId);
            if (member == null)
            {
                ViewData["Error"] = "There is no known user.";
                return Redirect("/Login.aspx");
            }
            _problemService.AddResponse(model.ProblemId, model.Text, member);
            return RedirectToAction("Details", new { id = model.ProblemId });
        }

        [OpenIdAuthorize]
        [HttpGet]
        public ActionResult Close(String id)
        {
            var member = _memberRepository.Get(MemberInformations.OpenId);
            _problemService.CloseProblem(id, member);
            return RedirectToAction("Index", "Home");
        }

        [OpenIdAuthorize]
        [HttpGet]
        public ActionResult Up(String id, String responseId)
        {
            var member = _memberRepository.Get(MemberInformations.OpenId);
            _problemService.UpResponse(id, responseId, member);
            return RedirectToAction("Details", new { id });
        }

        [OpenIdAuthorize]
        [HttpGet]
        public ActionResult Down(String id, String responseId)
        {
            var member = _memberRepository.Get(MemberInformations.OpenId);
            _problemService.DownResponse(id, responseId, member);
            return RedirectToAction("Details", new { id });   
        }

    }
}
