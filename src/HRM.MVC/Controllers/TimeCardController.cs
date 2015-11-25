using System;
using Microsoft.AspNet.Mvc;

using HRM.MVC.ViewModels.TimeCard;
using HRM.Domain.Repository;
using HRM.ApplicationLayer;

namespace HRM.MVC.Controllers
{
    public class TimeCardController : Controller
    {
         private readonly  IUnitOfWork uow;

        public TimeCardController( IUnitOfWork _uow)
        {
            this.uow = _uow;
            
        }

        // GET: TimeCard
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Clocking()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Clocking( ClockingViewModel vm, string button)
        {
            var tcs = new TimeCardServices(uow);
            switch (button)
            {
                case "ClockIn":
                    var result1 =  tcs.StartWorkSession(uow, DateTime.Now, vm.UserCode ) ;
                    if (!result1.Success){ ModelState.AddModelError(string.Empty, result1.Message);}
                    else { vm.Message = "Clocked in"; }
                    break;
                case "ClockOut":
                    var result2 = tcs.EndWorkSession(uow,   DateTime.Now, vm.UserCode);
                    if( !result2.Success) { ModelState.AddModelError(string.Empty, result2.Message); }
                    else { vm.Message = "Clocked out"; }
                    break;
            }
                        return View();
        }

}
}
