using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Mvc.Rendering;


    namespace HRM.MVC.ViewModels.Admin
{
        public class AddWageSchemaForUserViewModel : BaseViewModel 
    {
        public string SelectedUser { get; set; }
public IEnumerable<SelectListItem> Users{ get; set; }

        [Required]
        [Display(Name = "Titel")]
        public string Title { get; set; }

[Required]
        [Display(Name = "Timlön")]
        public double HourlyWage { get; set; }

}
}