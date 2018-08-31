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
        public void Index()
        {
            Test();
        }

        void Test()
        {
            var response = new VoiceResponse();

            response.Say("This application does not support voice recordings.");

            Console.WriteLine(response.ToString()); ;
        }

        [HttpPost("success")]
        public TwiMLResult Success(string RecordingUrl, int RecordingDuration, string Digits)
        {
             var response = new VoiceResponse( );
            response.Say("Thank you for recording your response. Have a nice day");

            return TwiML(response);
        }
    }
}
