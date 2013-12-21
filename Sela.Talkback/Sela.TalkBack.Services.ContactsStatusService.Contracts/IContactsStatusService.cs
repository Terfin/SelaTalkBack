using Sela.TalkBack.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Sela.TalkBack.Services.ContactsStatusService.Contracts
{
    [ServiceContract(Namespace=@"http://www.selatalkback.com")]
    public interface IContactsStatusService
    {
        [OperationContract(IsOneWay=true)]
        void ChangeUserStatus(string userHash, bool online);

        [OperationContract]
        List<User> GetOnlineContacts();
    }
}
