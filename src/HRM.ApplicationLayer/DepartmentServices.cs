using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRM.Domain.Repository;
using HRM.Domain.Model;
using HRM.Patterns;

namespace HRM.ApplicationLayer
{
    public class DepartmentServices
    {
        private IUnitOfWork uow;

        public DepartmentServices(IUnitOfWork _uow )
        {
            uow = _uow;
        }

        public IBasicResult  SaveDepartment(string CompanyID, string title, string description)
        {
            var dep = uow.DepartmentRepository.GetEagerLoad( d => d.CompanyID.Equals(CompanyID, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            if (dep == null)
            {
                uow.DepartmentRepository.Insert(new Department()
                { CompanyID = CompanyID, Title = title, Description = description });
                uow.Save();
                return new BasicResult(true, "the department added", null   );
            }
            else
            {
                return new BasicResult(false, "the department already exists", null);
            }
        }

    }
}
