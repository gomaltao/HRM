using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRM.Domain.Repository;
using HRM.Domain.Model;


namespace HRM.Domain.Services
{
    public class WageDomainServices
    {
        private IUnitOfWork uow;

        public WageDomainServices( IUnitOfWork _uow )
        {
            uow = _uow;
        }

        public bool VerifyStartAndEndTime(WageSchemaDetail detail  )
        {
            var invalidDetail  = uow.WageSchemaDetailsRepository.GetEagerLoad( w =>
            Enum.Equals(w.Day, detail.Day)
            && (
            (detail.StartTime >= w.StartTime && detail.StartTime < w.EndTime)
            || (detail.EndTime > w.StartTime && detail.EndTime <= w.EndTime))
            ).FirstOrDefault();
            return invalidDetail  != null ? false : true;
        }
    }
}
