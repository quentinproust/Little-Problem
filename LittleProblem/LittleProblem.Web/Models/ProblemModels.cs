using System;
using System.ComponentModel.DataAnnotations;

namespace LittleProblem.Web.Models
{

    #region Models
    public class ProblemModel
    {
        [Required(ErrorMessage = "You need to enter a title.")]
        public string Title { get; set; }
        [Required(ErrorMessage = "You should explain your problem.")]
        public string Text { get; set; }
    }

    public class ResponseModel
    {
        [Required(ErrorMessage = "An empty response isn't a response.")]
        public string Text { get; set; }

        public string ProblemId { get; set; }
    }

    public class ActionOnProblemModel
    {
        public string ProblemId { get; set; }
    }
    #endregion

}
