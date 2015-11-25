using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Domain.Model
{

    public class WageSchema
    {
        public int WageSchemaID { get; set; }
        public string Title { get; set; }
        public string CustomerAdminMail { get; set; }
        public double HourlyWage { get; set; }
        public int Level { get; set; }
public ICollection<WageSchemaDetail> WageSchemaDetails{get; set;} 
        public int ProfessionID { get; set; }
        public Profession Profession { get; set; }
        public string UserID { get; set; }
        public User employee { get; set; }

        public WageSchema()
        {
            WageSchemaDetails = new HashSet<WageSchemaDetail>();
        }
    }        
}
