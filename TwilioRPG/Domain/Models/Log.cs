using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
   public class Log
    {

        public int Id { get; set; }
        public DateTime DateUtc { get; set; }
        public string LogLevel { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }
        public string Message { get; set; }
        public string Exception { get; set; }
        public string Controller { get; set; }
        public string Method { get; set; }
        public Guid Conversation { get; set; }
    }
}
