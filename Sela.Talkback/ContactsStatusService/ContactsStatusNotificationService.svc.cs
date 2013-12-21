using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using Sela.TalkBack.Services.ContactsStatusService.Contracts;
using System.Text;
using ContactsLogics;

namespace ContactsStatusService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ContactsStatusNotificationService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ContactsStatusNotificationService.svc or ContactsStatusNotificationService.svc.cs at the Solution Explorer and start debugging.
    [ServiceBehavior(InstanceContextMode=InstanceContextMode.Single)]
    public class ContactsStatusNotificationService : IContactsStatusNotificationService, IDisposable
    {
        ContactsOperations ops;
        Dictionary<string, IContactsStatusNotificationsCallback> callbacksMap = new Dictionary<string, IContactsStatusNotificationsCallback>();

        public ContactsStatusNotificationService()
        {
            ops = new ContactsOperations(true, On_UsersCacheChanged);
        }

        public void Subscribe(string userHash)
        {
            if (!callbacksMap.ContainsKey(userHash))
            {
                callbacksMap[userHash] = OperationContext.Current.GetCallbackChannel<IContactsStatusNotificationsCallback>();
            }
        }

        public void UnSubscribe(string userHash)
        {
            if (callbacksMap.ContainsKey(userHash))
            {
                callbacksMap.Remove(userHash);
            }
        }

        public void On_UsersCacheChanged(object sender, List<StateChangedEventArgs> args)
        {
            foreach (string key in callbacksMap.Keys)
            {
                if (args.Count == 1)
                {
                    callbacksMap[key].UserStatusChangedNotification(args[0]);
                }
                else if (args.Count > 1)
                {
                    callbacksMap[key].UserBundleNotification(args);
                }
                else
                {
                    break;
                }
            }
            
        }


        public void Dispose()
        {
            ops.Dispose();
        }
    }
}
