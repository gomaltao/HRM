using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc.Rendering;
using System.Security.Claims;
using HRM.MVC.ViewModels.Admin;
using HRM.Domain.Repository;
using HRM.Domain.Model;
using HRM.Infrastructure.Identity;
using HRM.ApplicationLayer;
using HRM.Infrastructure;
using HRM.Patterns;
using System.Globalization;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace HRM.MVC.Controllers
{
    public class AdminController : Controller
    {
        private IUnitOfWork uow;
        private UserManager<ApplicationUser> userManager;


        public AdminController(IUnitOfWork _uow, UserManager<ApplicationUser> _userManager)
        {
            uow = _uow;
            userManager = _userManager;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "SuperAdmin")]
        public IActionResult AddCustomerAdmin()
        {
            AddCcustomerAdminViewModel vm = new AddCcustomerAdminViewModel();
            vm.Users = from u in userManager.Users
                       select new SelectListItem
                       {
                           Text = u.Email,
                           Value = u.Email
                       };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> AddCustomerAdmin(AddCcustomerAdminViewModel vm)
        {
            var SelectedUser = await userManager.FindByEmailAsync(vm.SelectedValue);
            var exists = await userManager.IsInRoleAsync(SelectedUser, "CustomerAdmin");
            if (!exists)
            {
                var result = await userManager.AddToRoleAsync(SelectedUser, "CustomerAdmin");
                if (result.Succeeded)
                {
                    vm.Message = "user added to customer admins";
                }
                else
                {
                    vm.Message = "user could not become a customer admin";
                }
            }
            else
            {
                vm.Message = "user is already a customer admin";
            }
            vm.Users = from u in userManager.Users
                       select new SelectListItem
                       {
                           Text = u.Email,
                           Value = u.Email
                       };
            return View(vm);
        }

        
                [Authorize(Roles = "SuperAdmin, CustomerAdmin")]
                public IActionResult AddCompany()
                {
                    return View();
                }

                [HttpPost]
                [ValidateAntiForgeryToken]
                [Authorize(Roles = "SuperAdmin, CustomerAdmin")]
                public async Task<IActionResult> AddCompany(AddCompanyViewModel vm)
                {
                    if (ModelState.IsValid)
                    {
                        var CurrentUser = await GetCurrentUserAsync();
                        CompanyServices compServ = new CompanyServices(uow);
                        var result = compServ.Create(CurrentUser.Email, vm.CompanyID, vm.Companyname, vm.Email, vm.Address, vm.PhoneNumber);
                        if (result.Success)
                        {
                            return RedirectToAction("index", "home");
                        }
                        ModelState.AddModelError(string.Empty, result.Message);
                        return View(vm);
                    }
                    ModelState.AddModelError(string.Empty, "Incorrect input");
                    return View(vm);
                }
        
                [Authorize(Roles = "SuperAdmin, CustomerAdmin")]
                public async Task<IActionResult> AddDepartment()
                {
                    var currentUser = await GetCurrentUserAsync();
                    var vm = new AddDepartmentViewModel();
                    vm.Companies = from c in uow.CompanyRepository.GetEagerLoad(w => w.CustomerAdminMail.Equals(currentUser.Email))
                                   select new SelectListItem
                                   {
                                       Text = c.CompanyName,
                                       Value = c.CompanyID
                                   };
                    return View(vm);
                }
        
                [HttpPost]
                [ValidateAntiForgeryToken]
                [Authorize(Roles = "SuperAdmin, CustomerAdmin")]
                public async Task<IActionResult> AddDepartment(AddDepartmentViewModel vm)
                {
                    var currentUser = await GetCurrentUserAsync();
                    vm.Companies = from c in uow.CompanyRepository.GetEagerLoad(w => w.CustomerAdminMail.Equals(currentUser.Email))
                                   select new SelectListItem
                                   {
                                       Text = c.CompanyName,
                                       Value = c.CompanyID
                                   };

                    if (ModelState.IsValid)
                    {
                        var depService = new DepartmentServices(uow);
                        var result = depService.SaveDepartment(vm.SelectedValue, vm.Title, vm.Description);
                        if (result.Success) { vm.Message = result.Message; }
                        else { ModelState.AddModelError(string.Empty, result.Message); }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "input incorrect");
                    }
                    return View(vm);
                }
        [HttpGet]
                [Authorize(Roles = "SuperAdmin, CustomerAdmin")]
                public IActionResult AddStandardUser()
                {
                    return View();
                }
        
                [HttpPost]
                [ValidateAntiForgeryToken]
                [Authorize(Roles = "SuperAdmin, CustomerAdmin")]
                public IActionResult AddStandardUser(AddStandardUserViewModel vm)
                {
            if (ModelState.IsValid)
            {
                var adminServices = new AdminServices(uow);
                var result = adminServices.AddStandardUser(vm.SSN, vm.FirstName, vm.LastName, vm.PhoneNumber, vm.UserName, vm.UserCode, vm.Email );
                if (result.Success)
                {
                    vm.Message = result.Message;
                    return View(vm);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, result.Message);
                    return View(vm);
                }
            }
            ModelState.AddModelError(string.Empty, "Incorrect input");
                    return View(vm);
                }

        [Authorize(Roles = "SuperAdmin, CustomerAdmin")]
        public async Task<IActionResult> AddProfession()
        {
            var appUser = await GetCurrentUserAsync();
            var compServ = new CompanyServices(uow);
            var  result =compServ.GetCompaniesForUser(appUser.Email);            
            if( !result.Success )
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return View(); 
            }
                AddProfessionViewModel vm = new AddProfessionViewModel();
            vm.Companies = CreateCompanyList(result.ReturnValue);
            
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin, CustomerAdmin")]
        public async Task<IActionResult> AddProfession(AddProfessionViewModel  vm)
        {
            var compServ = new CompanyServices(uow);
            var appUser = await GetCurrentUserAsync();
            var compResult = compServ.GetCompaniesForUser(appUser.Email);
            vm.Companies = CreateCompanyList(compResult.ReturnValue);
            if ( ModelState.IsValid)
            {
                var currentUser = uow.UserRepository.GetEagerLoad(u => u.Email.Equals(appUser.Email, StringComparison.OrdinalIgnoreCase) ).FirstOrDefault();
                var adminServices = new AdminServices(uow);
                var result = adminServices.Addprofession(appUser.Email,  vm.SelectedValue,   currentUser.UserID , vm.Title, vm.Description); 
if( result.Success)
                {
                    vm.Message = result.Message;
                    return View(vm); 
                }
                else
                {
                    ModelState.AddModelError(string.Empty, result.Message);
                    return View(vm);
                }
            }
            ModelState.AddModelError(string.Empty, "incorrect input");
            return View(vm);
        }


        [Authorize(Roles = "SuperAdmin, CustomerAdmin")]
        public async Task<IActionResult> AddWageSchemaForProfession()
        {
            AddWageSchemaForProfessionViewModel vm = new AddWageSchemaForProfessionViewModel();
            var compServ = new CompanyServices(uow);
            var currentUser = await GetCurrentUserAsync();
            var compResult = compServ.GetCompaniesForUser(currentUser.Email);
            vm.Companies = CreateCompanyList(compResult.ReturnValue);
            vm.Professions = new SelectList(new List<SelectListItem>(), "Value", "Text"); 
                return View(vm);
        }

        public JsonResult GetProfessions(string CompanySelected)
        {
            var profServ = new ProfessionServices(uow);
            var result = profServ.GetProfessionForCompany(CompanySelected);
            AddWageSchemaForProfessionViewModel vm = new AddWageSchemaForProfessionViewModel();
if( result.Success)
            {
                vm.Professions = from p in result.ReturnValue
                                 select new SelectListItem { Text = p.Title, Value = p.ProfessionID.ToString() }; 
            }
            return Json(new {vm.Professions  }); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin, CustomerAdmin")]
        public async Task<IActionResult> AddWageSchemaForProfession(AddWageSchemaForProfessionViewModel  vm)
        {

            var adminServ = new AdminServices(uow);

            var compServ = new CompanyServices(uow);
            var currentUser = await GetCurrentUserAsync();
            var compResult = compServ.GetCompaniesForUser(currentUser.Email);
            vm.Companies = CreateCompanyList(compResult.ReturnValue);
            vm.Professions = new SelectList(new List<SelectListItem>(), "Value", "Text");

            if (ModelState.IsValid)
            {
                var result = adminServ.AddWageSchema(Convert.ToInt32( vm.SelectedProfession) , vm.Title, vm.HourlyWage, vm.Level);
                if (result.Success)
                {
                    vm.Message = result.Message;
                    return View(vm);
                }
                ModelState.AddModelError(string.Empty, result.Message);
                return View(vm);
            }
            ModelState.AddModelError(string.Empty, "incorrect input");
            return View(vm);
        }


        [Authorize(Roles = "SuperAdmin, CustomerAdmin")]
        public async Task<IActionResult> AddWageSchemaForUser()
        {
            var adminServ = new AdminServices(uow);
            var currentAdmin = await GetCurrentUserAsync();
            return View();

        }

        [Authorize(Roles = "SuperAdmin, CustomerAdmin")]
        public async Task<IActionResult> AddWageSchemaDetails()
        {
            var adminServ = new AdminServices(uow);
            var appUser = await GetCurrentUserAsync();
            AddWageSchemaDetailsViewModel vm = new AddWageSchemaDetailsViewModel();
            vm.Days = GetDays();
            
            vm.StartHours = GetHours();
            vm.StartMinutes = GetMinutes();
            vm.EndHours = GetHours();
            vm.EndMinutes = GetMinutes();
            var result = adminServ.GetWageSchemasForUser(appUser.Email);
            if (!result.Success)
            {
                vm.Message = result.Message;
                vm.wageSchemas = new SelectList( new List<SelectListItem>(), "WageSchemaID", "Title" );
                return View(vm);
            }
            vm.wageSchemas = new SelectList(result.ReturnValue, "WageSchemaID", "Title");
                return View(vm);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin, CustomerAdmin")]
        public async Task<IActionResult> AddWageSchemaDetails( AddWageSchemaDetailsViewModel vm )
        {
                var adminServ = new AdminServices(uow);
                var appUser = await GetCurrentUserAsync();

            if ( ModelState.IsValid)
            {
                var res = adminServ.AddWageSchemaDetails(vm.SelectedWageSchema,  vm.SelectedDay, vm.StartHour, vm.StartMinute, vm.EndHour, vm.EndMinute   ); 
            }
            vm.Days = GetDays();
            vm.StartHours = GetHours();
            vm.StartMinutes = GetMinutes();
            vm.EndHours = GetHours();
            vm.EndMinutes = GetMinutes();
            var result = adminServ.GetWageSchemasForUser(appUser.Email);
            if (!result.Success)
            {
                vm.Message = result.Message;
                vm.wageSchemas = new SelectList(new List<SelectListItem>(), "WageSchemaID", "Title");
                return View(vm);
            }
            vm.wageSchemas = new SelectList(result.ReturnValue, "WageSchemaID", "Title");
            return View(vm);
        }

private SelectList GetDays()
        {
            var enumData = from DayOfWeek d in Enum.GetValues(typeof(WeekDaysEnum))
                           select new
                           {
                               ID = (int)d,
                               Name = d.ToString()
                           };
 return                new SelectList(enumData, "ID", "Name");
        }

        private SelectList GetWageSchemas( CollectionResult<WageSchema> result)
        {
            return null;
            {
            }

        }
        private SelectList GetHours()
        {
            var HourData = from int h in Enumerable.Range(0, 24)
                           select new
                           {
                               ID = h,
                               Name = h.ToString()
                           };
            return new SelectList(HourData, "ID", "Name");

        }
        private SelectList GetMinutes()
        {
            var MinutesData = from int h in Enumerable.Range(0, 60)
                           select new
                           {
                               ID = h,
                               Name = h.ToString()
                           };
            return new SelectList(MinutesData, "ID", "Name");
        }


        private IEnumerable<SelectListItem> CreateCompanyList(IEnumerable<Company> companies )
        {
            return  from c in companies 
                           select new SelectListItem{
Text = c.CompanyName, Value = c.CompanyID};
        }

        private async Task<ApplicationUser> GetCurrentUserAsync()
        {
            return await userManager.FindByIdAsync(HttpContext.User.GetUserId());
        }


    }
}
