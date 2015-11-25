using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HRM.Domain.Repository;


    namespace HRM.Domain.Model
{
    public class TimeCard
    {
        public int TimeCardID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public TimeCardStatusEnum timeCardEnum { get; set; }
        public string UserID { get; set; }
        public User employee { get; set; }

    }        
}
