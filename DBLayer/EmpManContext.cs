using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Entity;
using DBLayer.Model;

namespace DBLayer
{
    public class EmpManContext : DbContext
    {
        public DbSet<Employee> {get; set;}
    }
}
