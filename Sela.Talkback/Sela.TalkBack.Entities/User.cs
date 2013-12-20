using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Sela.TalkBack.Common.Entities
{
    [DataContract(Namespace = @"http://www.selatalkback.com")]
    public class User
    {
        public event EventHandler OnLogInTimeout;
        private Timer timer;
        private bool loggedIn = false;

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            timer.Stop();
            timer.Dispose();
            timer = null;
            if (OnLogInTimeout != null)
            {
                OnLogInTimeout(this, null);
            }
        }

        [DataMember(IsRequired = true)]
        public string Username { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public string UserHash { get; set; }
        [DataMember]
        public bool IsLoggedIn
        {
            get
            {
                return loggedIn;
            }
            set
            {
                loggedIn = value;
                if (value == true)
                {
                    if (timer == null)
                    {
                        timer = new Timer();
                        timer.Elapsed += timer_Elapsed;
                        timer.Start();
                    }
                }
            }
        }

        public void ConfirnKeepAlive()
        {
        }
    }
}
