using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc.ViewFeatures;
using Microsoft.AspNet.Mvc.Rendering;

namespace HRM.MVC.ViewModels.Admin
{
    public class AddCcustomerAdminViewModel : BaseViewModel
    {

        public string SelectedValue { get; set; }
        public IEnumerable<SelectListItem> Users { get; set; }

    }
}
