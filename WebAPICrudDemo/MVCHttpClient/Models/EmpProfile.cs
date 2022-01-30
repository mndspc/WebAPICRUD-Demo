using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCHttpClient.Models
{
    public class EmpProfile
    {
        public int EmpCode { get; set; }

        public string EmpName { get; set; }

        public System.DateTime DateOfBirth { get; set; }

        public string Email { get; set; }

        public int DeptCode { get; set; }
    }
}