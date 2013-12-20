using Sela.TalkBack.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsLogics
{
    public class ContactsOperations
    {
        private UsersCache cache;
        public ContactsOperations()
        {
            cache = UsersCache.Instance;
        }

        public List<User> GetOnlineUsers()
        {
            return cache.GetUsers(x => x.IsLoggedIn);
        }

        public void ChangeUserOnlineState(string userHash, bool online)
        {
            
        }
    }
}
