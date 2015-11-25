using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Data.Entity;
using HRM.Infrastructure.Data;
using HRM.Domain.Repository;
using HRM.Domain.Model;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNet.Builder;
namespace HRM.Infrastructure.Repository
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private DbContext context;

        public UnitOfWork()
        {
            context = new HRMContext();
        }

        private GenericRepository<User> _UserRepository;
        public IGenericRepository<User> UserRepository
        {
            get
            {
                _UserRepository = _UserRepository ?? new GenericRepository<User>(context);
                return _UserRepository; 
            }
            private set { }
        }

        private GenericRepository<TimeCard> _TimeCardRepository;
        public IGenericRepository<TimeCard> TimeCardRepository
        {
            get
            {
                _TimeCardRepository = _TimeCardRepository ?? new GenericRepository<TimeCard>(context);
                return _TimeCardRepository; 
            }
            private set { }
        }

        private GenericRepository<Company> _CompanyRepository;
        public IGenericRepository<Company> CompanyRepository
        {
            get
            {
                _CompanyRepository = _CompanyRepository ?? new GenericRepository<Company>(context);
                return _CompanyRepository;
            }
            private set { }
        }

        private GenericRepository<WageSchema> _WageSchemaRepository;
        public IGenericRepository<WageSchema> WageSchemaRepository
        {
            get
            {
                _WageSchemaRepository = _WageSchemaRepository ?? new GenericRepository<WageSchema>(context);
                return _WageSchemaRepository;
            }
            private set { }
        }

        private GenericRepository<Department> _DepartmentRepository;
        public IGenericRepository<Department> DepartmentRepository
        {
            get
            {
                _DepartmentRepository = _DepartmentRepository ?? new GenericRepository<Department>(context);
                return _DepartmentRepository;
            }
            private set { }
        }

        private GenericRepository<Profession> _ProfessionRepository;
        public IGenericRepository<Profession> ProfessionRepository
        {
            get
            {
                _ProfessionRepository=  _ProfessionRepository?? new GenericRepository<Profession>(context);
                return  _ProfessionRepository ;
            }
            private set { }
        }

        private GenericRepository<WageSchemaDetail> _WageSchemaDetailsRepository;
        public IGenericRepository<WageSchemaDetail> WageSchemaDetailsRepository
        {
            get
            {
                _WageSchemaDetailsRepository = _WageSchemaDetailsRepository ?? new GenericRepository<WageSchemaDetail>(context);
                return _WageSchemaDetailsRepository;
            }
            private set { }
        }




        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
