using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRM.Domain.Repository;
using HRM.Domain.Model;
using HRM.Patterns;


namespace HRM.ApplicationLayer
{
    public class ProfessionServices
    {
        private IUnitOfWork uow;

        public ProfessionServices(IUnitOfWork _uow)
        {
            uow = _uow;
        }
public  CollectionResult<Profession> GetProfessionForCompany( string OrgID )
        {
            var profs = uow.ProfessionRepository.GetEagerLoad( p => p.CompanyID.Equals(OrgID, StringComparison.OrdinalIgnoreCase));
            if( profs == null)
            {
                return new  CollectionResult<Profession>(false, null, "no professions", null);
            }
            return new CollectionResult<Profession>(true, profs, "professions found", null);
        }
    }
}
