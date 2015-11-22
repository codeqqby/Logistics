using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Logistics.Models
{
    public class UserQueryModel
    {
        public int id { get; set; }
        public string uname { get; set; }
        public string rname { get; set; }
        public string phone { get; set; }
        public string isadmin { get; set; }
    }
}