using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace HRM.MVC.ViewModels.Admin
{
    public class AddStandardUserViewModel : BaseViewModel 
    {

        [Required]
        public string SSN { get; set; }

        public string Email { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string UserCode { get; set; }


    }
}
