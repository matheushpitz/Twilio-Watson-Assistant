using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwilioWatsonProject.Services
{
    public interface IWatsonService
    {
        string SendToAssistant(string message);
    }
}
