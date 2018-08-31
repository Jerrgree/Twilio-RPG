using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twilio.AspNet.Common;
using Twilio.AspNet.Core;
using Twilio.TwiML;
using Domain;
using TwilioRPG.Common;
using NLog.Fluent;
using NLog;

namespace TwilioRPG.Controllers
{
    [Route("api/[controller]")]
    public class TextController : TwilioController
    {
        private RPGContext context;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public TextController()
        {
            context = new RPGContext();
        }

        [HttpPost]
        public TwiMLResult Index(SmsRequest request)
        {
            var Conversation = Guid.NewGuid();

            try
            {
                logger.Info()
                        .Message(String.Format("Message recieved.\nBody: {0}", request.Body))
                        .Property("Source", request.From)
                        .Property("Destination", request.To)
                        .Property("Conversation", Conversation)
                        .Property("Date", DateTime.UtcNow)
                        .Write();
            }
            catch (Exception ex)
            {

                throw;
            }

            var messagingResponse = new MessagingResponse();

            switch (request.Body.ToLower().Trim())
            {
                case "register":
                    return Register(request, Conversation);
                case "helpme":
                    return Help(request, Conversation);
            }

            logger.Info()
                .Message("No Matching Command found")
                .Property("Source", request.From)
                .Property("Destination", request.To)
                .Property("Conversation", Conversation)
                .Property("Date", DateTime.UtcNow)
                .Property("Controller", "Text")
                .Property("Method", "Index")
                .Write();
            messagingResponse.Message("No matching command found. To view available commands, reply with 'HelpMe'");

            return TwiML(messagingResponse);
        }

        TwiMLResult Register(SmsRequest request, Guid Conversation)
        {
            logger.Info()
                .Message("Method: Register")
                .Property("Source", request.From)
                .Property("Destination", request.To)
                .Property("Conversation", Conversation)
                .Property("Date", DateTime.UtcNow)
                .Property("Controller", "Text")
                .Property("Method", "Register")
                .Write();

            var messagingResponse = new MessagingResponse();

            if (Utilities.UserExists(request.From))
            {
                messagingResponse.Message("This number is already associated with a user.");

                logger.Info()
                .Message("Unable to register as User already exists")
                .Property("Source", request.From)
                .Property("Destination", request.To)
                .Property("Conversation", Conversation)
                .Property("Date", DateTime.UtcNow)
                .Property("Controller", "Text")
                .Property("Method", "Register")
                .Write();
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

                logger.Info()
                .Message("Succesfully registered new User")
                .Property("Source", request.From)
                .Property("Destination", request.To)
                .Property("Conversation", Conversation)
                .Property("Date", DateTime.UtcNow)
                .Property("Controller", "Text")
                .Property("Method", "Register")
                .Write();
            }

            return TwiML(messagingResponse);
        }

        TwiMLResult Help(SmsRequest request, Guid Conversation)
        {
            logger.Info()
                .Message("Method: Help")
                .Property("Source", request.From)
                .Property("Destination", request.To)
                .Property("Conversation", Conversation)
                .Property("Date", DateTime.UtcNow)
                .Property("Controller", "Text")
                .Property("Method", "Help")
                .Write();

            var messagingResponse = new MessagingResponse();

            messagingResponse.Message("You can currently use these commands:\nRegister\n");

            return TwiML(messagingResponse);
        }

    }
}
