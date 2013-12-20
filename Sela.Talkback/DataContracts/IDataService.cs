using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Sela.TalkBack.Common.Entities;

namespace DataContracts
{
    [ServiceContract]
    public interface IDataService
    {
        [OperationContract]
        User GetUser(string username, string password, string appKey);

        [OperationContract]
        User GetUserByHash(string userhash, string appKey);

        [OperationContract]
        string RegisterUser(User user, string appKey);

        [OperationContract]
        List<User> GetUsers(string appKey);
    }
}
