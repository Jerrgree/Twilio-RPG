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
using System.Threading;

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

            Logging.WriteLog(String.Format("Message recieved.\nBody: {0}", request.Body), request, Conversation, logger);

            var messagingResponse = new MessagingResponse();

            try
            {
                switch (request.Body.ToLower().Trim())
                {
                    case "register":
                        return Register(request, Conversation);
                    case "helpme":
                        return Help(request, Conversation);
                }
            }
            catch (Exception ex)
            {
                Logging.WriteLog("Exception thrown", request, Conversation, logger, "Index", "Text", Common.LogLevel.Error, ex);

                messagingResponse.Message("I'm sorry, but an error has occured.");
                return TwiML(messagingResponse);
            }

            Logging.WriteLog("No Matching Command found", request, Conversation, logger);

            messagingResponse.Message("No matching command found. To view available commands, reply with 'HelpMe'");

            return TwiML(messagingResponse);
        }

        TwiMLResult Register(SmsRequest request, Guid Conversation)
        {
            Logging.WriteLog("Method: Register", request, Conversation, logger, "Register", "Text");

            var response = new MessagingResponse();

            if (Utilities.UserExists(request.From, context))
            {
                response.Message("This number is already associated with a user.");

                Logging.WriteLog("Unable to register as User already exists", request, Conversation, logger, "Register", "Text");
            }
            else
            {
                try
                {
                    context.Add(new Domain.Models.User()
                    {
                        Number = request.From,
                        IsActive = true,
                        CreateDateUtc = DateTime.UtcNow
                    });

                    var result = context.SaveChanges();

                    response.Message("You have succesfully registered!");

                    Logging.WriteLog("Succesfully registered new User", request, Conversation, logger, "Register", "Text");
                }
                catch (Exception ex)
                {
                    Logging.WriteLog("An error occured attempting to add new user", request, Conversation, logger, "Register", "Text", Common.LogLevel.Error, ex);

                    response.Message("I'm sorry, but an error has occured.");
                }
            }

            Logging.WriteLog("Ending Conversation", request, Conversation, logger, "Register", "Text");

            return TwiML(response);
        }

        TwiMLResult Help(SmsRequest request, Guid Conversation)
        {
            Logging.WriteLog("Method: Help", request, Conversation, logger, "Register", "Help");

            var messagingResponse = new MessagingResponse();

            messagingResponse.Message("You can currently use these commands:\nRegister\n");


            Logging.WriteLog("Ending Conversation", request, Conversation, logger, "Register", "Help");
            return TwiML(messagingResponse);
        }
    } 
}
