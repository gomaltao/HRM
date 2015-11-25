using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRM.Domain.Model
{
    public class ProfessionUser
    {
        public int ProfessionID { get; set; }
        public string UserID { get; set; }
        public Profession profession { get; set; }
        public User user { get; set; }

    }
}
