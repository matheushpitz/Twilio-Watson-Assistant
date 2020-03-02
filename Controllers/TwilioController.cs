using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Twilio.TwiML;
using Twilio.AspNet.Core;
using TwilioWatsonProject.Services;

namespace TwilioWatsonProject.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TwiliooController : TwilioController
    {
        private readonly ITwilioService _service;
        private readonly IWatsonService _watsonService;

        public TwiliooController(ITwilioService service, IWatsonService watsonService)
        {
            _service = service;
            _watsonService = watsonService;
        }

        [HttpPost("Send")]
        public TwiMLResult Send()
        {
            string body = Request.Form.Where((f) => f.Key.Equals("Body")).FirstOrDefault().Value;
            string to = Request.Form.Where((f) => f.Key.Equals("To")).FirstOrDefault().Value;
            string from = Request.Form.Where((f) => f.Key.Equals("From")).FirstOrDefault().Value;

            var response = new MessagingResponse();

            //string[] command = body.Split(";");
            //if (command.Length == 3 && command[0].Equals("send"))
            //{
            //_service.SendMessage(command[1], command[2], "+14155238886");
            //response.Message("Message successfully sent!");
            //}
            //else
            //{                
            //response.Message("Thanks for sending message to HTC. Your message:\n  *" + body + "*\nFrom: *" + from + "*\nTo: *" + to + "*");
            //response.Message("You can send a message for anyone using the command 'send;{MESSAGE};{Phone Number}'");
            //response.Message("For instance:");
            //response.Message("send;Hi Helton;+551199999999");
            //}            
            response.Message(this._watsonService.SendToAssistant(body));

            return TwiML(response);
        }
    }
}