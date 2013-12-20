using Sela.TalkBack.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Sela.TalkBack.LoginService.Contracts
{
    [ServiceContract(Namespace = @"http://www.selatalkback.com")]
    public interface ILoginService
    {
        [OperationContract(IsOneWay = true)]
        void LogoutUser(User user);

        [OperationContract]
        List<User> SignUpUser(User user);

        [OperationContract]
        List<User> LoginUser(User user);
    }
}
