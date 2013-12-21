using Sela.TalkBack.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Sela.TalkBack.Services.ContactsStatusService.Contracts
{
    [ServiceContract(CallbackContract=typeof(IContactsStatusNotificationsCallback))]
    public interface IContactsStatusNotificationService
    {
        [OperationContract(IsOneWay = true)]
        void Subscribe(string userHash);

        [OperationContract(IsOneWay = true)]
        void UnSubscribe(string userHash);
    }

    [ServiceContract]
    public interface IContactsStatusNotificationsCallback
    {
        [OperationContract(IsOneWay=true)]
        void UserStatusChangedNotification(StateChangedEventArgs change);

        [OperationContract(IsOneWay=true)]
        void UserBundleNotification(IEnumerable<StateChangedEventArgs> changes);
    }
}
