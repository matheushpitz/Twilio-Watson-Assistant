using IBM.Cloud.SDK.Core.Authentication.Iam;
using IBM.Cloud.SDK.Core.Http;
using IBM.Watson.Assistant.v2;
using IBM.Watson.Assistant.v2.Model;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using TwilioWatsonProject.Session;

namespace TwilioWatsonProject.Services
{
    public class WatsonService : IWatsonService
    {
        private readonly string apiToken;
        private readonly string apiServiceUrl;
        private readonly string apiAssistantId;
        private readonly Dictionary<string, WatsonSession> session;
        private AssistantService _service;        

        public WatsonService(IConfiguration config)
        {
            this.apiToken = config.GetSection("watson").GetSection("token").Value;
            this.apiServiceUrl = config.GetSection("watson").GetSection("service_url").Value;
            this.apiAssistantId = config.GetSection("watson").GetSection("assistant_id").Value;

            this.session = new Dictionary<string, WatsonSession>();

            IamAuthenticator authenticator = new IamAuthenticator(this.apiToken);

            _service = new AssistantService("2020-02-05", authenticator);
            _service.SetServiceUrl(this.apiServiceUrl);            
        }

        private string GetOrCreateSession(string number)
        {
            if (this.session.ContainsKey(number) && this.session.GetValueOrDefault(number).isValid())
            {
                return this.session.GetValueOrDefault(number).getSessionId();
            } else
            {
                this.session.Remove(number);
                DetailedResponse<SessionResponse> session = _service.CreateSession(this.apiAssistantId);
                this.session.Add(number, new WatsonSession(session.Result.SessionId));
                return session.Result.SessionId;
            }
        }

        public IEnumerable<string> SendToAssistant(string message, string fromNumber)
        {                                    
            DetailedResponse<MessageResponse> result = _service.Message(this.apiAssistantId, this.GetOrCreateSession(fromNumber), new MessageInput()
            {
                Text = message
            });            

            return result.Result.Output.Generic.Select(g => g.Text);
        }
    }
}
