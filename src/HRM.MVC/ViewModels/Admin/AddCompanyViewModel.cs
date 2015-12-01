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
        [Display(Name="Organisationsnummer" )]
        public string CompanyID { get; set; }

        [Required]
        [Display(Name="företagets namn" )]
        public string Companyname { get; set; }
        [Display(Name ="Adress")]
        public string Address { get; set; }

        [Display(Name ="Telefonnummer")]
        public string PhoneNumber { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name ="Email")]
        public string Email { get; set; }


    }
}
