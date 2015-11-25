using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace HRM.MVC.ViewModels
{
    public class BaseViewModel
    {
        [ScaffoldColumn(false)]
        public string Message { get; set; }

    }
}
