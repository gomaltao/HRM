using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRM.Domain.Repository;
using HRM.Domain.Model;
using HRM.Patterns;

namespace HRM.ApplicationLayer.Properties
{
    public class ProfessionServices
    {
        private IUnitOfWork uow;

        public ProfessionServices(IUnitOfWork _uow )
        {
            uow = _uow;
        }

        public  ICollectionResult<Profession> GetProfessionsForUser(string UserEmail )
        {
            return new CollectionResult<Profession>(false, null , "no", null);
        }
    }
}
