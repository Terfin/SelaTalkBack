using ContactsLogics;
using Sela.TalkBack.Common.Entities;
using Sela.TalkBack.Services.ContactsStatusService.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ContactsStatusService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ContactsStatusService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ContactsStatusService.svc or ContactsStatusService.svc.cs at the Solution Explorer and start debugging.
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class ContactsStatusService : IContactsStatusService
    {
        public void ChangeUserStatus(string userHash, bool online)
        {
            using (ContactsOperations ops = new ContactsOperations())
            {
                try
                {
                    ops.ChangeUserOnlineState(userHash, online);
                }
                catch (InvalidOperationException e)
                {
                    throw new FaultException(e.Message);
                }
            }
        }

        public List<User> GetOnlineContacts()
        {
            using (ContactsOperations ops = new ContactsOperations())
            {
                return ops.GetOnlineUsers();
            }
        }
    }
}
