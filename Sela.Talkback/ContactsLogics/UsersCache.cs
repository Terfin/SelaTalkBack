using DataContracts;
using Sela.TalkBack.Common.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
using System.Web;

namespace ContactsLogics
{
    internal class UsersCache
    {
        private Dictionary<string, WeakReference<User>> users;
        private string appKey;
        private static UsersCache instance;
        static object syncroot = new object();

        private UsersCache()
        {
            users = new Dictionary<string, WeakReference<User>>();
            appKey = ConfigurationManager.AppSettings["appKey"];
        }

        internal static UsersCache Instance
        {
            get
            {
                lock (syncroot)
                {
                    if (instance == null)
                    {
                        instance = new UsersCache();
                    }
                }
                return instance;
            }
        }

        internal User this[string userhash]
        {
            get
            {
                lock (users)
                {
                    User userToReturn = null;
                    if (users[userhash].TryGetTarget(out userToReturn))
                    {
                        return userToReturn;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            set 
            {
                lock (users[userhash])
                {
                    users[userhash].SetTarget(value);
                }
            }
        }

        internal void Insert(string userHash, User user)
        {
            lock (users)
            {
                if (users[userHash] == null)
                {
                    users[userHash] = new WeakReference<User>(user);
                }
                else
                {
                    users[userHash].SetTarget(user);
                }
            }
        }

        internal void Remove(string userHash)
        {
            lock (users)
            {
                users.Remove(userHash);
            }
        }

        internal void Update(string userHash, Action<User> updateAct)
        {
            lock (users)
            {
                if (users.ContainsKey(userHash))
                {
                    User user;
                    if (users[userHash].TryGetTarget(out user))
                    {
                        updateAct(user);
                    }
                    else
                    {
                        var cf = new ChannelFactory<IDataService>("dataservice");
                        var channel = cf.CreateChannel();
                        channel.GetUserByHash(userHash, 
                    }
                }
            }
        }

        internal List<User> GetUsers(Predicate<User> pred)
        {
            lock (users)
            {
                return users.Values.Where(x =>
                {
                    User user;
                    if (x.TryGetTarget(out user))
                    {
                        return pred(user);
                    }
                    else
                    {
                        return false;
                    }
                }).Select(x =>
                {
                    User user;
                    if (x.TryGetTarget(out user))
                    {
                        return user;
                    }
                    return null;
                }).ToList();
            }
        }
    }
}