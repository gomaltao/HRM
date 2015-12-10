using System;
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

        public BasicResult AddStandardUser(string adminEmail,   string UserID, string firstName, string lastName, string phoneNumber, string userName, string userCode, string email = "" )
        {
            var usr = uow.UserRepository.GetEagerLoad(w => 
            w.UserID.Equals(UserID, StringComparison.OrdinalIgnoreCase) 
            && w.AdminEmail.Equals( adminEmail, StringComparison.OrdinalIgnoreCase)
            ).FirstOrDefault();
            if (usr != null)
            {
                return new BasicResult(false, "user already exists", null);
            }
                var user = new User()
                { AdminEmail = adminEmail,  UserID = UserID, FirstName = firstName, LastName = lastName, PhoneNumber = phoneNumber, UserName = userName, UserCode = userCode, Email = email  };
                uow.UserRepository.Insert(user);
                uow.Save();
                return new BasicResult(true, "user created", null);
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
        public BasicResult AddWageSchema(int   professionID, string title,  double hourlyWage )
        {
            var currentWageSchema = uow.WageSchemaRepository.GetEagerLoad( ws => ws.ProfessionID == professionID && ws.Title.Equals(title, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            if( currentWageSchema != null)
            {
                return new BasicResult(false, "wage schema already exists", null);
            }
            currentWageSchema = new WageSchema()
            { ProfessionID = professionID, Title = title, HourlyWage = hourlyWage };
            uow.WageSchemaRepository.Insert(currentWageSchema);
            uow.Save();
            return new BasicResult(true, "wage  schema added", null);
        }

        public CollectionResult<WageSchema>   GetWageSchemasForUserByProfession( string email )
        {
            var currentUser = uow.UserRepository.GetEagerLoad( u => u.Email.Equals( email, StringComparison.OrdinalIgnoreCase) , includeProperties: inc => inc.professionUsers  ).FirstOrDefault();
            if( currentUser == null)
            {
                return new CollectionResult<WageSchema>(false, null, "denna användare har inte tilldelats någon peersonal användare", null );
            }
            var profsIDs  = currentUser.professionUsers.Where(w => w.UserID.Equals(currentUser.UserID, StringComparison.OrdinalIgnoreCase)).Select( s => s.ProfessionID ) ;
            if( profsIDs== null )
            {
                return new CollectionResult<WageSchema>( false, null, "denna personal har inte tilldelats något yrke", null );
            }
            var WageSchemaList = uow.WageSchemaRepository.GetEagerLoad( ws => profsIDs.Contains(ws.ProfessionID ?? -1  ));
            if ( WageSchemaList == null )
            {
                return new CollectionResult<WageSchema>(false, null, "inget löneschema för denna personal", null );
            }
            return new CollectionResult<WageSchema>(true, WageSchemaList.AsEnumerable(), "wage schema  found", null );
        }

        public CollectionResult<WageSchema> GetWageSchemasForUser(string email)
        {
            var currentUser = uow.UserRepository.GetEagerLoad(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            if(currentUser == null)
            {
                return new CollectionResult<WageSchema>( false, null, "ingen personal definierad för denna användare", null );
            }
            var WageSchemas = uow.WageSchemaRepository.GetEagerLoad( w => !string.IsNullOrEmpty(w.CustomerAdminMail) && w.CustomerAdminMail.Equals(email , StringComparison.OrdinalIgnoreCase ));
            if( WageSchemas == null )
            {

                return new CollectionResult<WageSchema>(false, null , "inga lönescheman för denna användare", null );
            }
            return new CollectionResult<WageSchema>(true,  WageSchemas, "löneschamn funna", null);
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
            //var wageDS = new WageDomainServices(uow);
            //var validDetail = wageDS.VerifyStartAndEndTime(details);
            if (!valid)
            {
                return new BasicResult(false, "överlappande period för " + day.ToString(), null);
            }
            uow.WageSchemaDetailsRepository.Insert(details);
            uow.Save();
            return new BasicResult(true, "wage schema info addded", null);
        }

        public CollectionResult<User>  GetUsersForAdmin( string AdminMail )
        {
            var users = uow.UserRepository.GetEagerLoad( u => u.AdminEmail.Equals(AdminMail, StringComparison.OrdinalIgnoreCase   )); 
            if( users == null || users.Count() == 0 )
            {
                return new CollectionResult<User>(false, null , "inga anställda för denna admin", null);
            }
            return new CollectionResult<User>( true, users , "anställda hittade", null);
        }

        public BasicResult  AddWageSchemaForUser(string AdminEmail,  string UserID, string Title, double HourlyWage )
        {
            var ws = uow.WageSchemaRepository.GetEagerLoad( w => w
            .UserID.Equals(UserID, StringComparison.OrdinalIgnoreCase ) 
            && w.Title.Equals( Title, StringComparison.OrdinalIgnoreCase))
            .FirstOrDefault();
if( ws != null)
            {
                return new BasicResult(false, "det finns redan ett löneschema med titeln " + Title + " för denna anställda", null);
            }
            ws = new WageSchema();
            ws.Title = Title;
            ws.UserID = UserID;
            ws.HourlyWage = HourlyWage;
            ws.CustomerAdminMail = AdminEmail;
            uow.WageSchemaRepository.Insert(ws);
            uow.Save();
            return new BasicResult(true, "löneschamt tillagt", null);

        }

        public CollectionResult<WageSchemaDetail>  GetWageSchemaDetailsForUser( int WageSchemaID   )
        {
            var details = uow.WageSchemaDetailsRepository.GetEagerLoad(w => w.WageSchemaID == WageSchemaID);
            if( details == null )
            {
                return new CollectionResult<WageSchemaDetail>(false, null, "inga schema detaljer tillagda", null);
            }
            return new CollectionResult<WageSchemaDetail>(true, details, "lönteschema detaljer hittade", null  );
        }

    } // class 
} // namespace
