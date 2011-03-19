using System;
using System.ComponentModel.DataAnnotations;

namespace LittleProblem.Web.Models
{

    #region Models
    public class LogInModel
    {
        public string OpenId { get; set; }
    }

    public class ProfileModel
    {
        [Required(ErrorMessage = "You need to have a user name.")]
        public String UserName { get; set; }
        public String Email { get; set; }
    }
    #endregion

}
