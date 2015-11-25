using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Mvc.Rendering;


    namespace HRM.MVC.ViewModels.Admin
{
        public class AddWageSchemaForProfessionViewModel : BaseViewModel 
    {
        public string SelectedCompany { get; set; }
public IEnumerable<SelectListItem> Companies { get; set; }
        public string SelectedProfession { get; set; }
        public IEnumerable<SelectListItem> Professions { get; set; }

        [Required]
        public string Title { get; set; }

        public double HourlyWage { get; set; }

        public int Level { get; set; }


}
}