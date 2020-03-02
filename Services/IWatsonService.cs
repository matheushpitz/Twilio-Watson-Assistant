using System;
using System.Collections.Generic;

namespace TwilioWatsonProject.Services
{
    public interface IWatsonService
    {
        IEnumerable<String> SendToAssistant(string message, string fromNumber);
    }
}
