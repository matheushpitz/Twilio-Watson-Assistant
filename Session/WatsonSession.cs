using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwilioWatsonProject.Session
{
    public class WatsonSession
    {
        private string SessionId { get; set; }
        private long TouchTime { get; set; }

        public WatsonSession(string sessionId)
        {
            SessionId = sessionId;
            TouchTime = DateTime.Now.Ticks;
        }

        public bool isValid()
        {
            return DateTime.Now.Ticks < (TouchTime + 300000000);            
        }

        public string getSessionId()
        {
            TouchTime = DateTime.Now.Ticks;
            return SessionId;
        }
    }
}
