using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRM.Patterns;
using HRM.Domain.Model;
using HRM.Domain.Repository;
namespace HRM.ApplicationLayer
{

public     class HourlyRatePeriodServices
    {

        private IUnitOfWork uow;
        public HourlyRatePeriodServices(IUnitOfWork _uow )
        {
            uow = _uow;

        }
        public IResult<WageSchema> Create(string title, double hourlyPay, int level)
        {
            WageSchema sc = new WageSchema()
            { Title = title, HourlyWage = hourlyPay, Level = level };
            this.uow.WageSchemaRepository.Insert(sc);
            this.uow.Save();
            return new APIResult<WageSchema>(null, true, "success", null);
        }
}
}
