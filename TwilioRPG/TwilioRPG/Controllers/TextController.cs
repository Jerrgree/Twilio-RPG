using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twilio.AspNet.Common;
using Twilio.AspNet.Core;
using Twilio.TwiML;
using Domain;

namespace TwilioRPG.Controllers
{
    [Route("api/[controller]")]
    public class TextController : TwilioController
    {
        RPGContext context;

        public TextController()
        {
            context = new RPGContext();
        }

        [HttpPost]
        public TwiMLResult Index(SmsRequest request)
        {
            var messagingResponse = new MessagingResponse();

            switch (request.Body.ToLower().Trim())
            {
                case "register":
                    return Register(request);
                case "helpme":
                    return Help(request);
            }

            messagingResponse.Message("No matching command found. To view available commands, reply with 'HelpMe'");

            return TwiML(messagingResponse);
        }

        TwiMLResult Register(SmsRequest request)
        {
            var messagingResponse = new MessagingResponse();

            var userAlreadyExists = context.Users
                                    .Where(x => x.Number == request.From)
                                    .Any();
            if (userAlreadyExists)
            {
                messagingResponse.Message("This number is already associated with a user.");
            }
            else
            {
                context.Add(new Domain.Models.User()
                {
                    Number = request.From,
                    IsActive = true,
                    CreateDateUtc = DateTime.UtcNow
                });

                var result = context.SaveChanges();

                messagingResponse.Message("User succesfully Registered!");
            }

            return TwiML(messagingResponse);
        }

        TwiMLResult Help(SmsRequest request)
        {
            var messagingResponse = new MessagingResponse();

            messagingResponse.Message("You can currently use these commands:\nRegister\n");

            return TwiML(messagingResponse);
        }

    }
}
