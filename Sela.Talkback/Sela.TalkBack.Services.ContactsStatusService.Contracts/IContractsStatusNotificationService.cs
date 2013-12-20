using Sela.TalkBack.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Sela.TalkBack.Services.ContactsStatusService.Contracts
{
    [ServiceContract(CallbackContract=typeof(IContractsStatusNotificationsCallback))]
    public interface IContractsStatusNotificationService
    {
        [OperationContract(IsOneWay=true)]
        void SignUpForNotifications();
    }

    [ServiceContract]
    public interface IContractsStatusNotificationsCallback
    {
        [OperationContract(IsOneWay=true)]
        void UserStatusChangedNotification(User user);

        [OperationContract(IsOneWay=true)]
        void UserBundleNotification(IEnumerable<User> users);
    }
}
