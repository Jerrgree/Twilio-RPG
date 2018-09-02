using NLog;
using NLog.Fluent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twilio.AspNet.Common;

namespace TwilioRPG.Common
{
    public enum LogLevel
    {
        Debug,
        Info,
        Warn,
        Error,
        Fatal
    };

    public class Logging
    {
        public static void WriteLog(string message, SmsRequest request, Guid conversation, Logger logger, string Method="", string Controller="", LogLevel logLevel = LogLevel.Info, Exception ex = null )
        {
            LogBuilder logBuilder;

            switch (logLevel)
            {
                case LogLevel.Debug:
                    logBuilder = logger.Debug();
                    break;
                case LogLevel.Warn:
                    logBuilder = logger.Warn();
                    break;
                case LogLevel.Error:
                    logBuilder = logger.Error();
                    break;
                case LogLevel.Fatal:
                    logBuilder = logger.Error();
                    break;
                case LogLevel.Info:
                default:
                    logBuilder = logger.Info();
                    break;
            }

            logBuilder
                .Message(message)
                .Exception(ex)
                .Property("Source", request.From)
                .Property("Destination", request.To)
                .Property("Conversation", conversation)
                .Property("Date", DateTime.UtcNow)
                .Property("Controller", Controller)
                .Property("Method", Method)
                .Write();
        }
    }
}
