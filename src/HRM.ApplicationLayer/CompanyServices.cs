using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRM.Patterns;
using HRM.Domain.Repository;
using HRM.Domain.Model;

namespace HRM.ApplicationLayer
{
    public class CompanyServices
    {
        private IUnitOfWork uow;

        public CompanyServices(IUnitOfWork _uow )
        {
            uow = _uow;
        }


        public IBasicResult  Create(string customerAdmin,  string CompanyID,  string companyName, string email, string address, string phoneNumber )
        {
            var user = uow.UserRepository.GetEagerLoad(u => u.Email.Equals(customerAdmin, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            if (user == null)
            {
                return new BasicResult(false, "no user with  email: " + customerAdmin, null);
            }
            if( uow.CompanyRepository.GetEagerLoad(c => c.CompanyID.Equals(CompanyID, StringComparison.OrdinalIgnoreCase)).FirstOrDefault() != null)  
                {
                return new BasicResult(false, "company already exists", null);
            }
            Company company = new Company()
            { CustomerAdminMail = customerAdmin,  CompanyID = CompanyID,   CompanyName = companyName, Address = address , Email = email, PhoneNumber = phoneNumber };
            var cu = new CompanyUser() { UserID = user.UserID, CompanyID = CompanyID };  
            company.CompanyUsers.Add(cu);
            uow.CompanyRepository.Insert(company);
            uow.Save();
            return new BasicResult(true, "a new company has been added", null);

        }


        public CollectionResult<Company>    GetCompaniesForUser( string UserEmail)
        {
            string message = string.Empty;

            var currentUser = uow.UserRepository.GetEagerLoad(u => u.Email.Equals( UserEmail, StringComparison.OrdinalIgnoreCase), includeProperties: i => i.CompanyUsers).FirstOrDefault();
            if (currentUser == null)
            {
                return new CollectionResult<Company>(false, null, "the user does not exists", null);
            }
            if (currentUser.CompanyUsers == null)
            {
                return new CollectionResult<Company>(false, null, "no companies for " + UserEmail, null);  
            }
            var CompaniesIDs = currentUser.CompanyUsers.Select(o => o.CompanyID).ToList();
            var companies = uow.CompanyRepository.GetEagerLoad(c => CompaniesIDs.Contains(c.CompanyID));

            /*
                        if (companies.Count() == 0)
                        {
                            ModelState.AddModelError(string.Empty,
                                message = "no company for this admin";
                            return null;
                        }
                        */

            return new CollectionResult<Company>(true, companies, "companies for " + UserEmail, null);
        }

    }
}
