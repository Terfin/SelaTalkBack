using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Sela.TalkBack.Common.Entities
{
    [DataContract]
    public abstract class NotificationResponseBase
    {
        [DataMember]
        public DateTime SendDate { get; set; }
    }
}
