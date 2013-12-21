using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using DataContracts;

namespace Sela.TalkBack.Services.DataLayerService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "DataNotificationService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select DataNotificationService.svc or DataNotificationService.svc.cs at the Solution Explorer and start debugging.
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, InstanceContextMode = InstanceContextMode.Single)]
    public class DataNotificationService : INotifyDataService
    {
        private Dictionary<string, INotifyDataServiceCallback> callbackMap = new Dictionary<string, INotifyDataServiceCallback>();

        public void Subscribe(string userHash)
        {
            var notifyCallback = OperationContext.Current.GetCallbackChannel<INotifyDataServiceCallback>();
            callbackMap[userHash] = notifyCallback;
        }

        public void UnSubscribe(string userHash)
        {
            callbackMap.Remove(userHash);   
        }
    }
}
