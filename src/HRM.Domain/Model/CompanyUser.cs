using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRM.Domain.Model
{
    public class CompanyUser
    {
        public string CompanyID { get; set; }
        public Company company   { get; set; }
        public string UserID { get; set; }
        public User Employee { get; set; }
    }

}
