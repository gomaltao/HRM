using System;
using System.Linq;
using HRM.Patterns;

using HRM.Domain.Repository;
using HRM.Domain.Model;
using HRM.Infrastructure.Repository;

namespace HRM.ApplicationLayer
{
    public class TimeCardServices
    {
        private IUnitOfWork  uow;

        public TimeCardServices( IUnitOfWork _uow )
        {
            uow = _uow;
        }

        public APIResult<TimeCard> StartWorkSession(IUnitOfWork uow, DateTime StartDate, string UserCode)
        {
            var emp = uow.UserRepository.GetEagerLoad(e => e.UserCode.Equals(UserCode, StringComparison.OrdinalIgnoreCase)).FirstOrDefault(); 
            if (emp == null)
            {
                return new APIResult<TimeCard>(null, false, "no employee with this user code: " + UserCode, null);
            }

            var tc = uow.TimeCardRepository.GetEagerLoad(t =>
            (t.UserID != null) &&
           t.UserID.Equals(emp.UserID, StringComparison.OrdinalIgnoreCase)
           , orderBy: q => q.OrderByDescending(p => p.StartDate)
           ).FirstOrDefault();
            if (tc != null && tc.timeCardEnum.Equals(TimeCardStatusEnum.open))
            {
                return new APIResult<TimeCard>(null, false, "already an opened time card", null);
            }
            tc = new TimeCard();
            tc.UserID = emp.UserID; 
            tc.StartDate = StartDate;
            tc.timeCardEnum = TimeCardStatusEnum.open;
            uow.TimeCardRepository.Insert(tc);
            uow.Save();
            return new APIResult<TimeCard>(tc, true, "a new time card is created", null);
        }

        public APIResult<TimeCard> EndWorkSession(IUnitOfWork uow, DateTime EndDate, string UserCode)
        {
            var currentUser  = uow.UserRepository.GetEagerLoad(emp => emp.UserCode.Equals(UserCode, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            if( currentUser == null )
            {
                return new APIResult<TimeCard>(null, false, "no user for this time card", null );
            }
            var tc = uow.TimeCardRepository.GetEagerLoad( t => 
            t.UserID.Equals(currentUser.UserID, StringComparison.OrdinalIgnoreCase)
             && t.timeCardEnum.Equals(TimeCardStatusEnum.open) 
            , orderBy: q => q.OrderByDescending(p => p.StartDate))
            //, includeProperties: prop => prop.employee)
            .FirstOrDefault();

            if (tc != null)
            {
                tc.EndDate = EndDate;
                tc.timeCardEnum = TimeCardStatusEnum.close;
                uow.Save();
                return new APIResult<TimeCard>(tc, true, "time card closed", null);
            }
            else
            {
                return new APIResult<TimeCard>(null, false, "no time card to close", null);
            }
        }

    }
}
