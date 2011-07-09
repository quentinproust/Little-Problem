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
            if (id < 0)
            {
                throw new HttpException(404, "Could not find the requested page. Pagination number can't be negative !");
            }

            ViewData["Message"] = "Welcome to LittleProblems !";

            var lastProblems = _problemRepository.All(id);
            ViewData["NbProblem"] = _problemRepository.Count();

            return View(lastProblems);
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
