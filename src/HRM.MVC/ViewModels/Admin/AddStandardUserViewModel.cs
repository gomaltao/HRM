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
        [Display(Name ="Personnummer")]
        public string UserID { get; set; }

        [Display(Name ="Email")]
        public string Email { get; set; }

        
        [Required]
        [Display(Name = "Användarnamn")]
        public string UserName { get; set; }

        [Required]
        [Display(Name ="Förnamn")]
        public string FirstName { get; set; }
        
        [Required]
        [Display(Name = "Efternamn")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Telefonnummer")]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Användarkod")]
        public string UserCode { get; set; }


    }
}
