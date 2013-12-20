using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sela.TalkBack.Common.Entities
{
    public class NotificationRequest<T>
    {
        public DateTime UpdateDate { get; set; }
        public T RequestMessage { get; set; }
    }
}
