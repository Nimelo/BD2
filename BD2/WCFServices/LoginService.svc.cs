using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Database;
using Common.Enums;
using Common;

namespace WCFServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "LoginService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select LoginService.svc or LoginService.svc.cs at the Solution Explorer and start debugging.
    public class LoginService : ILoginService
    {
        public UserAccess Login(string login, string password)
        {
            bool successful = false;
            User u = null;
            try
            {
                using(var db = new DatabaseContainer())
                {
                    u = db.Users.Where(x => x.Login == login).First();

                    if(Encryption.Match(password, u.Password))
                    {
                        successful = true;
                    }

                }
            }
            catch(Exception e)
            {

            }
          
            if(successful && u != null)
            {
                return (UserAccess)u.Role;
            }
            else
            {
                throw new Exception("There is no user!");
            }

        }



        public bool CanLogin(string login, string password)
        {
            bool successful = false;
            User u = null;
            try
            {
                using(var db = new DatabaseContainer())
                {
                    u = db.Users.Where(x => x.Login == login
                        && x.Role == (byte)UserRolesEnum.Candidate).First();

                    if(Encryption.Match(password, u.Password))
                    {
                        successful = true;
                    }

                }
            }
            catch(Exception e)
            {

            }

            return successful;
        }
    }
}
