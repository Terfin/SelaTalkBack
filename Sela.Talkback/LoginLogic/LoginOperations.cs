using DataContracts;
using Sela.TalkBack.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Sela.TalkBack.Services.ContactsStatusService.Contracts;

namespace LoginLogic
{
    public class LoginOperations
    {
        private string appKey;
        public LoginOperations()
        {
            appKey = ConfigurationManager.AppSettings["appKey"];
        }
        public bool PerformLogin(User user)
        {
            
            var cf = new ChannelFactory<IDataService>("dataservice");
            var channel = cf.CreateChannel();
            var userAuthenticated = false;
            try
            {
                channel.GetUser(user.Username, user.Password, appKey);
                userAuthenticated = true;
            }
            catch (FaultException e)
            {
                userAuthenticated = false;
            }
            finally
            {
                if (cf.State == CommunicationState.Opened)
                {
                    cf.Close();
                }
            }
            return userAuthenticated;
        }

        public bool TrySignUp(User user, out string error)
        {
            error = "";
            bool signUpSuccessful = false;
            var cf = new ChannelFactory<IDataService>("dataservice");
            var channel = cf.CreateChannel();
            try
            {
                channel.RegisterUser(user, appKey);
                signUpSuccessful = true;
            }
            catch (FaultException e)
            {
                error = e.Message;
            }
            finally
            {
                if (cf.State == CommunicationState.Opened)
                {
                    cf.Close();
                }
            }
            return signUpSuccessful;
        }

        public List<User> GetUsers(bool onlineOnly)
        {
            if (onlineOnly)
            {
                var cf = new ChannelFactory<IContactsStatusService>("contactsservice");
                var channel = cf.CreateChannel();
                var users = channel.GetOnlineContacts();
                if (cf.State == CommunicationState.Opened)
                {
                    cf.Close();
                }
                return users;
            }
            else
            {
                var cf = new ChannelFactory<IDataService>("dataservice");
                var channel = cf.CreateChannel();
                var users = channel.GetUsers(appKey);
                if (cf.State == CommunicationState.Opened)
                {
                    cf.Close();
                }
                return users;
            }
            
        }
    }
}