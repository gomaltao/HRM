using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
    
namespace HRM.MVC.ViewModels.TimeCard
{
    public class ClockingViewModel :  BaseViewModel 
    {
        [Required]
        public string UserCode { get; set; }
    }
} 