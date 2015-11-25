using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HRM.Infrastructure.Identity
{
    public class ApplicationIdentityRole : IdentityRole 
    {

        public ApplicationIdentityRole(string Rolename )
            : base(Rolename)
        {
        }

    }
}
