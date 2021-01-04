using System;
using System.Collections.Generic;
using System.Text;

namespace InvoiceNinja
{
    public static class NinjaApiHelper 
    {
        public static string baseUrl = "https://ninja-dev.viborg.it/api/v1/";        
        public static Dictionary<string, string> headerTokens
        {
            get
            {
                return new Dictionary<string, string>()
               {
                    { "X-Ninja-Token", ""},

               };
            }
        }

    }
}
