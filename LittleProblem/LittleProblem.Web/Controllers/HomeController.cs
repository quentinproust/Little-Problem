using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.MobileControls;
using LittleProblem.Data;
using LittleProblem.Data.Model;
using LittleProblem.Data.Repository;
using LittleProblem.Data.Services;
using NLog;

namespace LittleProblem.Web.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        private readonly IProblemRepository _problemRepository;

        public HomeController(IProblemRepository problemRepository)
        {
            _problemRepository = problemRepository;
        }

        public ActionResult Index(int id = 0)
        {
            ViewData["Message"] = "Welcome to LittleProblems !";

            List<Problem> lastProblems = _problemRepository.All(id);
            int nbProblem = _problemRepository.Count();
            ViewData["NbProblem"] = nbProblem;

            return View(lastProblems);
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
