using DataContracts;
using Sela.TalkBack.Common.Entities;
using Sela.TalkBack.Services.ContactsStatusService.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsLogics
{
    public class ContactsOperations : IDisposable, INotifyDataServiceCallback
    {
        private UsersCache cache;
        private EventHandler<List<StateChangedEventArgs>> cacheHandler;
        private bool disposed = false;
        public ContactsOperations()
        {
            cache = UsersCache.Instance;
        }

        public ContactsOperations(bool subscribe, EventHandler<List<StateChangedEventArgs>> handler) : this()
        {
            if (subscribe)
            {
                cache.CacheStateChanged += handler;
                cacheHandler = handler;
            }
        }

        public List<User> GetOnlineUsers()
        {
            return cache.GetUsers(x => x.IsLoggedIn);
        }

        public void ChangeUserOnlineState(string userHash, bool online)
        {
            try
            {
                cache.Update(userHash, x => x.IsLoggedIn = online);
            }
            catch (InvalidOperationException e)
            {
                throw e;
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Dispose(true);
        }

        protected void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    if (cacheHandler != null)
                    {
                        cache.CacheStateChanged -= cacheHandler;
                        cacheHandler = null;
                    }
                }
                disposed = true;
            }
        }

        ~ContactsOperations()
        {
            Dispose(false);
        }

        /// Callback methods for pushes from Data Service

        public void AddUser(User user)
        {
            if (!cache.Contains(user))
            {
                cache.Add(user);
            }
        }

        public void RemoveUser(User user)
        {
            cache.Remove(user);
        }

        public void AddMultipleUsers(List<User> users)
        {
            users.Where(x => !cache.Contains(x)).ToList().ForEach(x => cache.Add(x));
        }

        public void RemoveMultipleUsers(List<User> users)
        {
            users.ForEach(x => cache.Remove(x));
        }

        /// End of Callback methods
    }
}
