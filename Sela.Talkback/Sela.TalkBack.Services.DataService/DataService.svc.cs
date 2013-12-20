using DataContracts;
using Sela.TalkBack.Common.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.ServiceModel;
using System.Text;

namespace Sela.TalkBack.Services.DataLayerService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "DataService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select DataService.svc or DataService.svc.cs at the Solution Explorer and start debugging.
    public class DataService : IDataService
    {
        public User GetUser(string username, string password, string appKey)
        {
            using (var hasher = SHA256.Create())
            {
                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(appKey))
                {
                    throw new FaultException("Bad Request");
                }
                var inptBytes = Encoding.ASCII.GetBytes(username + password);
                var hashBytes = hasher.ComputeHash(inptBytes);
                var userHash = Encoding.ASCII.GetString(hashBytes);
                using (var context = new SelaTalkBackEntities())
                {
                    if (context.AuthorizedApps.Any(x => x.AppKey == appKey))
                    {
                        EventLog.WriteEntry("DataService.GetUsers", string.Format("Unauthorized access attempt. Source: {0}, Key: {1}", OperationContext.Current.IncomingMessageHeaders.From.Uri.AbsoluteUri, appKey));
                        throw new FaultException(new FaultReason("Unauthorized"));
                    }
                    if (context.Members.Any(x => x.UserHash == userHash))
                    {
                        var dbUser = context.Members.Where(x => x.UserHash == userHash).Single();
                        return new User
                        {
                            Username = dbUser.Username
                        };
                    }
                    else
                    {
                        throw new FaultException<UserNotFoundException>(new UserNotFoundException());
                    }
                }
            }
        }

        public string RegisterUser(User user, string appKey)
        {
            if (string.IsNullOrEmpty(appKey))
            {
                throw new FaultException("Bad Request");
            }
            using (var hasher = SHA256.Create())
            {
                var inptBytes = Encoding.ASCII.GetBytes(user.Username + user.Password);
                var hashBytes = hasher.ComputeHash(inptBytes);
                user.UserHash = Encoding.ASCII.GetString(hashBytes);
                using (var context = new SelaTalkBackEntities())
                {
                    if (context.AuthorizedApps.Any(x => x.AppKey == appKey))
                    {
                        EventLog.WriteEntry("DataService.GetUsers", string.Format("Unauthorized access attempt. Source: {0}, Key: {1}", OperationContext.Current.IncomingMessageHeaders.From.Uri.AbsoluteUri, appKey));
                        throw new FaultException(new FaultReason("Unauthorized"));
                    }
                    if (!context.Members.Any(x => x.UserHash == user.UserHash))
                    {
                        context.Members.Add(new Member
                        {
                            Username = user.Username,
                            UserHash = user.UserHash,
                            Password = user.Password
                        });
                        context.SaveChanges();
                        return "OK";
                    }
                    else
                    {
                        throw new FaultException<UserExistsFault>(new UserExistsFault(500, string.Format("{0} already exists", user.Username)), new FaultReason("User already exists"));
                    }
                }
            }
        }


        public User GetUserByHash(string userhash, string appKey)
        {
            if (string.IsNullOrEmpty(userhash) || string.IsNullOrEmpty(appKey))
            {
                throw new FaultException("Bad Request");
            }
            using (var context = new SelaTalkBackEntities())
            {
                if (context.Members.Any(x => x.UserHash == userhash))
                {
                    var dbMember = context.Members.Where(x => x.UserHash == userhash).Single();
                    return new User
                    {
                        Username = dbMember.Username,
                        UserHash = dbMember.UserHash
                    };
                }
                else
                {
                    throw new FaultException<UserNotFoundException>(new UserNotFoundException());
                }
            }
        }


        public List<User> GetUsers(string appKey)
        {
            if (string.IsNullOrEmpty(appKey))
            {
                throw new FaultException(new FaultReason("Bad Request"));
            }
            using (var context = new SelaTalkBackEntities())
            {
                if (context.AuthorizedApps.Any(x => x.AppKey == appKey))
                {
                    return context.Members.Select(x => new User
                    {
                        Username = x.Username
                    }).ToList();
                }
                else
                {
                    EventLog.WriteEntry("DataService.GetUsers", string.Format("Unauthorized access attempt. Source: {0}, Key: {1}", OperationContext.Current.IncomingMessageHeaders.From.Uri.AbsoluteUri, appKey));
                    throw new FaultException(new FaultReason("Unauthorized"));
                }
            }
        }
    }
}
