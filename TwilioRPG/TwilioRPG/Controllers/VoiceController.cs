using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Twilio;
using System.Threading.Tasks;
using Twilio.AspNet.Core;
using Twilio.Http;
using Twilio.TwiML;

namespace TwilioRPG.Controllers
{
    [Route("api/[controller]")]
    public class VoiceController : TwilioController
    {
        [HttpPost]
        public TwiMLResult Index()
        {
            var response = new VoiceResponse();
            response
                .Say("Please leave a message at the beep.\nPress the pound key when finished.");
            response.Record(action: new Uri("https://ca22047f.ngrok.io/api/voice/success"),
                method: HttpMethod.Get, maxLength: 10, finishOnKey: "#");
            response.Say("I did not receive a recording");


            return TwiML(response);
        }

        [HttpGet("success")]
        public TwiMLResult Success()
        {
            var response = new VoiceResponse();
            response.Say("Thank you for recording your response. Have a nice day");

            return TwiML(response);
        }
    }
}
