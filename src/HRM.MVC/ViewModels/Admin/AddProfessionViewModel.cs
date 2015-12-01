﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Mvc.Rendering;

namespace HRM.MVC.ViewModels.Admin
{
    public class AddProfessionViewModel : BaseViewModel 
    {

        public string SelectedValue { get; set; }
        public IEnumerable<SelectListItem> Companies { get; set; }

        [Required]
        [Display(Name ="Beskrivning")]
        public string Description { get; set; }

        [Display(Name ="Titel" )]
    [Required ]
    public string Title { get; set; }
}
}
