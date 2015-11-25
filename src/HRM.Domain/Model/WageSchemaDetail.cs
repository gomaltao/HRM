using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using HRM.Domain.Repository;
namespace HRM.Domain.Model
{

    public class WageSchemaDetail
    {

        public int WageSchemaDetailID { get; private  set; }
        public int WageSchemaID { get; private  set; }
        public WageSchema  wageSchema { get; private  set; }
        public WeekDaysEnum Day { get; private  set; }
        public TimeSpan StartTime { get; private set; }
        public TimeSpan EndTime { get; private set; }
        private IUnitOfWork uow;


        public WageSchemaDetail(IUnitOfWork _uow,  int wageSchemaID,  WeekDaysEnum day, TimeSpan startTime, TimeSpan endTime )
        {
            uow = _uow;
            this.WageSchemaID = wageSchemaID;
            this.Day = day;
            this.StartTime = startTime;
            this.EndTime = endTime;
        }

    public bool Validate()
    {
        if (EndTime < StartTime) return false;
            var invalidDetail = uow.WageSchemaDetailsRepository.GetEagerLoad(w =>
              Enum.Equals(w.Day,  this.Day )
              && (
              (this.StartTime >= w.StartTime && this.StartTime < w.EndTime)
              || (this.EndTime > w.StartTime && this.EndTime <= w.EndTime))
                ).FirstOrDefault();
            return invalidDetail != null ? false : true;
        //return true;
    }

    }
}
