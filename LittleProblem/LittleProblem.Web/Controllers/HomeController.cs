using System.Web;
using System.Web.Mvc;
using LittleProblem.Data.Repository;
using LittleProblem.Web.Models;

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

            var lastProblems = _problemRepository.All(id);
            var nbProblems = _problemRepository.Count();

            return View(new ProblemListModel
                            {
                                Problems = lastProblems,
                                CurrentPage = id,
                                NbProblemsTotal = nbProblems
                            });
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
