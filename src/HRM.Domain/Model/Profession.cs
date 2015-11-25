using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRM.Domain.Model
{
    public class Profession
    {

        public int  ProfessionID {get; set;} 
        public string Title { get; set; }
        public string CustomerAdminMail { get; set; }
        public string Description { get; set; }
        public int WageSchemaID { get; set; }
        public ICollection<WageSchema> WageSchemas { get; set; }
        public string CompanyID { get; set; }
        public Company company { get; set; }
        public ICollection<ProfessionUser> professionUsers { get; set; }

        public Profession()
        {
            professionUsers = new HashSet<ProfessionUser>();
             WageSchemas = new HashSet<WageSchema>();
        }
    }
}
