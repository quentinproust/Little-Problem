using System.ComponentModel.DataAnnotations;
using Resources.Error;

namespace LittleProblem.Web.Models
{

    #region Models
    public class ProblemModel
    {
        [Required(ErrorMessageResourceType = typeof (Problem), ErrorMessageResourceName = "ProblemTitleIsRequired")]
        public string Title { get; set; }
        [Required(ErrorMessageResourceType = typeof (Problem), ErrorMessageResourceName = "ProblemMessageIsRequired")]
        public string Text { get; set; }
    }

    public class ResponseModel
    {
        [Required(ErrorMessageResourceType = typeof (Problem), ErrorMessageResourceName = "ResponseIsRequired")]
        public string Text { get; set; }

        public string ProblemId { get; set; }
    }
    #endregion

}
