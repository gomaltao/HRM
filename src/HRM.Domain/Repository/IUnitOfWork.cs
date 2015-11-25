using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRM.Domain.Model;

    namespace HRM.Domain.Repository
{
    public interface IUnitOfWork
    {
        IGenericRepository<User> UserRepository { get; }
        IGenericRepository<TimeCard> TimeCardRepository { get; }
        IGenericRepository<Company> CompanyRepository { get; }
        IGenericRepository<WageSchema> WageSchemaRepository { get; }
        IGenericRepository<Department> DepartmentRepository { get; }
        IGenericRepository<Profession> ProfessionRepository { get; }
        IGenericRepository<WageSchemaDetail> WageSchemaDetailsRepository { get; }  

        void Dispose();
            void Save();
}
}
