﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using HRM.Patterns;
using HRM.Domain.Repository;
using HRM.Domain.Model;
using HRM.Domain.Services;



namespace HRM.ApplicationLayer
{
    public class AdminServices
    {
        private IUnitOfWork uow;


        public AdminServices(IUnitOfWork _uow)
        {
            uow = _uow;
        }

        public BasicResult AddStandardUser(string UserID, string firstName, string lastName, string phoneNumber, string userName, string userCode, string email = "" )
        {
            var usr = uow.UserRepository.GetEagerLoad(w => w.UserID.Equals(UserID, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            if (usr == null)
            {
                // check if correct  UserID
                var user = new User()
                { UserID = UserID, FirstName = firstName, LastName = lastName, PhoneNumber = phoneNumber, UserName = userName, UserCode = userCode, Email = email  };
                uow.UserRepository.Insert(user);
                uow.Save();
                return new BasicResult(true, "user created", null);
            }
            else
            {
                return new BasicResult(false, "user already exists", null);
            }
        }

        public BasicResult Addprofession(string email,  string CompanyID, string UserID , string title, string description)
        {
            var prof = uow.ProfessionRepository.GetEagerLoad(p => p.Title.Equals(title, StringComparison.OrdinalIgnoreCase) 
            && p.company.CompanyID.Equals( CompanyID, StringComparison.OrdinalIgnoreCase ),   includeProperties: inc => inc.company ).FirstOrDefault();
            if( prof != null )
            {
                return new BasicResult(false, "profession already exists", null);
            }

            prof = new Profession()
            {CustomerAdminMail = email  ,  CompanyID = CompanyID, Title = title, Description = description };
            uow.ProfessionRepository.Insert(prof);
            var profUser = new ProfessionUser() { ProfessionID = prof.ProfessionID, UserID = UserID };
            prof.professionUsers.Add(profUser);
            uow.Save();
            return new BasicResult(true, "The profession  is added", null);
        }
        public BasicResult AddWageSchema(int   professionID, string title,  double hourlyWage, int level )
        {
            var currentWageSchema = uow.WageSchemaRepository.GetEagerLoad( ws => ws.ProfessionID == professionID && ws.Title.Equals(title, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            if( currentWageSchema != null)
            {
                return new BasicResult(false, "wage schema already exists", null);
            }
            currentWageSchema = new WageSchema()
            { ProfessionID = professionID, Title = title, HourlyWage = hourlyWage, Level = level       };
            uow.WageSchemaRepository.Insert(currentWageSchema);
            uow.Save();
            return new BasicResult(true, "wage  schema added", null);
        }

        public CollectionResult<WageSchema>   GetWageSchemasForUser( string email )
        {
            var currentUser = uow.UserRepository.GetEagerLoad( u => u.Email.Equals( email, StringComparison.OrdinalIgnoreCase) , includeProperties: inc => inc.professionUsers  ).FirstOrDefault();
            if( currentUser == null)
            {
                return new CollectionResult<WageSchema>(false, null, "no standard user ", null );
            }
            var profsIDs  = currentUser.professionUsers.Where(w => w.UserID.Equals(currentUser.UserID, StringComparison.OrdinalIgnoreCase)).Select( s => s.ProfessionID ) ;
            if( profsIDs== null )
            {
                return new CollectionResult<WageSchema>( false, null, "no professions for this user", null );
            }
            var WageSchemaList = uow.WageSchemaRepository.GetEagerLoad( ws => profsIDs.Contains(ws.ProfessionID ));
            if ( WageSchemaList == null )
            {
                return new CollectionResult<WageSchema>(false, null, "no wage schema  for this user", null );
            }
            return new CollectionResult<WageSchema>(true, WageSchemaList.AsEnumerable(), "wage schema  found", null );
        }


        public BasicResult AddWageSchema2(int professionID,   string title, double HourlyWage, int level   )
        {
            var hrp = new WageSchema( )
            {
                ProfessionID = professionID, 
                Title = title,
                HourlyWage = HourlyWage,
                Level = level 
            };
            uow.WageSchemaRepository.Insert(hrp);
            return new BasicResult(true, "wage schema added", null);
        }

        public BasicResult AddWageSchemaDetails(int wageSchemaID, int day, int startHour, int startMinute, int endHour, int endMinute )  
        {
            var startTime = new TimeSpan(startHour, startMinute, 0);
            var endTime = new TimeSpan(endHour, endMinute, 0);
            var dayEnum  = (WeekDaysEnum)day; 
            var details = new WageSchemaDetail(uow,  wageSchemaID,  dayEnum, startTime, endTime);
                var valid = details.Validate();
            if( !valid )
            {
                return  new BasicResult(false, "starttiden är större än sluttiden"  , null); 
            }
            var wageDS = new WageDomainServices(uow);
            var validDetail = wageDS.VerifyStartAndEndTime(details);
            if (!validDetail)
            {
                return new BasicResult(false, "överlappande period för " + day.ToString(), null);
            }
            uow.WageSchemaDetailsRepository.Insert(details);
            uow.Save();
            return new BasicResult(true, "wage schema info addded", null);
        }

        public BasicResult GetUsersForAdmin( string AdminMail )
        {
            return new BasicResult(true, "anställda hittade", null);
        }
    }
}
