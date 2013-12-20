using LoginLogic;
using Sela.TalkBack.Common.Entities;
using Sela.TalkBack.LoginService.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Sela.TalkBack.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "LoginService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select LoginService.svc or LoginService.svc.cs at the Solution Explorer and start debugging.
    public class LoginService : ILoginService
    {

        public void LogoutUser(User user)
        {
            ///TODO Add call contacts inner service
        }

        public List<User> SignUpUser(User user)
        {
            ValidateNulllArguments(user);
            LoginOperations ops = new LoginOperations();
            string err = null;
            if (ops.TrySignUp(user, out err))
            {
                List<User> users = ops.GetUsers(false);
                return users;
            }
            else
            {
                throw new FaultException();
            }
        }

        public List<User> LoginUser(User user)
        {
            ValidateNulllArguments(user);
            throw new LoginFailedException();
        }

        private void ValidateNulllArguments(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("No user supplied");
         
            }
            if (user.Username == null)
            {
                throw new ArgumentNullException("No username supplied");
         
            }
            if (user.Password == null)
            {
                throw new ArgumentNullException("No password supplied");
         
            }
         
        }
    }
}
