using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace HRM.Domain.Model
{
    public class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
        public string UserCode { get; set; }
        public string UserID { get; set; }
        public string Email { get; set; }
        public ICollection<TimeCard> TimeCards { get; set; }
        public ICollection<ProfessionUser> professionUsers { get; set; }
        public ICollection<CompanyUser> CompanyUsers { get; set; }
        public ICollection<WageSchema> WageSchemas { get; set; }

        public User()
        {
            TimeCards = new HashSet<TimeCard>();
            CompanyUsers = new HashSet<CompanyUser>();
            professionUsers = new HashSet<ProfessionUser>();
            WageSchemas = new HashSet<WageSchema>();

        }
    }
}