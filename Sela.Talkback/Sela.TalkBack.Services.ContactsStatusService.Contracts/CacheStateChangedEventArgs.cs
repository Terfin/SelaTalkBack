using Sela.TalkBack.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sela.TalkBack.Services.ContactsStatusService.Contracts
{
    public class CacheStateChangedEventArgs : EventArgs
    {
        public User User { get; set; }
        public CacheStateChange Change { get; set; }
    }
}
