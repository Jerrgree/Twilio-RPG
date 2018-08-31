using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwilioRPG.Common
{
    public class Utilities
    {
        static RPGContext context;

        static void InitializeDBContext()
        {
            if (context == null)
            {
                context = new RPGContext();
            }
        }

        public static bool UserExists(string number)
        {
            context = new RPGContext();

            return context.Users
                    .Where(x => x.Number == number)
                    .Any();
        }
    }
}
