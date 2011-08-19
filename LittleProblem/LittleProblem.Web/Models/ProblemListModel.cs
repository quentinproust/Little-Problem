using System.Collections.Generic;
using System.Linq;
using LittleProblem.Data.Model;

namespace LittleProblem.Web.Models
{
    public class ProblemListModel
    {

        public IEnumerable<Problem> Problems { get; set; }
        public int NbProblemsTotal { get; set; }
        public int CurrentPage { get; set; }

        public bool HasProblems { get { return Problems.Any(); } }
    }
}