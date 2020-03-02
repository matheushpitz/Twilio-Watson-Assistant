using Microsoft.Extensions.Configuration;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace TwilioWatsonProject.Services
{
    public class TwilioService : ITwilioService
    {

        private readonly string accountSid;
        private readonly string authToken;

        public TwilioService(IConfiguration config)
        {
            this.accountSid = config.GetSection("twilio").GetSection("account_sid").Value;
            this.authToken = config.GetSection("twilio").GetSection("token").Value;
        }

        public void SendMessage(string message, string to, string from)
        {
            TwilioClient.Init(this.accountSid, this.authToken);
            MessageResource.Create(
                from: new Twilio.Types.PhoneNumber("whatsapp:" + from),
                body: message,
                to: new Twilio.Types.PhoneNumber("whatsapp:" + to)
            );
        }
    }
}
