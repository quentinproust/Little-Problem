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
    #endregion

}
