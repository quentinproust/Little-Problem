using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LittleProblem.Data.Model
{
    public class Member
    {
        //public int _id { get; set; }
        public string OpenId { get; set; }
        public string UserName { get; set; }
        public DateTime LastConnection { get; set; }
        public DateTime FirstConnection { get; set; }
    }
}
