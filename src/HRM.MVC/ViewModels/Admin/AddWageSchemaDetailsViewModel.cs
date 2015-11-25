using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Mvc.Rendering;

namespace HRM.MVC.ViewModels.Admin
{
    public class AddWageSchemaDetailsViewModel : BaseViewModel
    {


        [Display(Name = "Select wage schema")]
        public int SelectedWageSchema { get; set; }
        public SelectList wageSchemas { get; set; }

        [Display(Name = "Select day")]
        public int SelectedDay { get; set; }
        public SelectList Days { get; set; }

        [Display(Name = "Start hour")]
        public int  StartHour { get; set; }
        public SelectList StartHours { get; set; }

        [Display(Name = "Start minute")]
        public int  StartMinute { get; set; }
        public SelectList StartMinutes { get; set; }

        [Display(Name = "End hour")]
        public int EndHour { get; set; }
        public SelectList EndHours { get; set; }

        [Display(Name = "End minute")]
        public int EndMinute { get; set; }
        public SelectList EndMinutes { get; set; }




    }
}
