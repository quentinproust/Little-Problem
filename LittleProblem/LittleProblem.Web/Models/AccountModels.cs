using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

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
