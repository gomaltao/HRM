using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Mvc.Rendering;
using HRM.Domain.Model;

namespace HRM.MVC.ViewModels.Admin
{
    public class AddWageSchemaDetailsViewModel : BaseViewModel
    {

        [Display(Name = "välj  löneschema ")]
        public int SelectedWageSchema { get; set; }
        public SelectList wageSchemas { get; set; }

        public List<WageSchemaDetail> ExistingDetails { get; set; } 

        [Display(Name = "välj dag")]
        public int SelectedDay { get; set; }
        public SelectList Days { get; set; }

        [Display(Name = "välj starttimme")]
        public int  StartHour { get; set; }
        public SelectList StartHours { get; set; }

        [Display(Name = "välj startminut")]
        public int  StartMinute { get; set; }
        public SelectList StartMinutes { get; set; }

        [Display(Name = "välj sluttimme")]
        public int EndHour { get; set; }
        public SelectList EndHours { get; set; }

        [Display(Name = "välj slutminut")]
        public int EndMinute { get; set; }
        public SelectList EndMinutes { get; set; }




    }
}
