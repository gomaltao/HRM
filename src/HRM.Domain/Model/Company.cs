using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Domain.Model
{
    public class Company
    {

        public string CompanyID { get; set; }
        public string CustomerAdminMail { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
public string Email { get; set; }
        public ICollection<Profession> Professions { get; set; }
        public ICollection<Department> Departments { get; set; }
        public ICollection<CompanyUser> CompanyUsers { get; set; }

        public Company()
        {
            CompanyUsers = new HashSet<CompanyUser>();
            Departments = new HashSet<Department>();
            Professions = new HashSet<Profession>();
        }
    }

}
