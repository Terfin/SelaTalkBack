using Sela.TalkBack.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace DataContracts
{
    [ServiceContract(CallbackContract=typeof(INotifyDataServiceCallback))]
    public interface INotifyDataService
    {
        [OperationContract]
        void Subscribe(string userHash);

        [OperationContract]
        void UnSubscribe(string userHash);
    }

    [ServiceContract]
    public interface INotifyDataServiceCallback
    {
        [OperationContract]
        void AddUser(User user);

        [OperationContract]
        void RemoveUser(User user);

        [OperationContract]
        void AddMultipleUsers(List<User> users);

        [OperationContract]
        void RemoveMultipleUsers(List<User> users);
    }


}
