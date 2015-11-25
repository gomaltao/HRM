using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using HRM.Domain.Model;

namespace HRM.MVC.ViewModels.Admin
{
    public class AddCompanyViewModel : BaseViewModel 
    {

        [Required]
        public string CompanyID { get; set; }

        [Required]
        public string Companyname { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


    }
}
