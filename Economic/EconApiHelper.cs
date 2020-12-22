using System;
using System.Collections.Generic;
using System.Text;

namespace Economic
{
    public static class EconApiHelper
    {
        public static string baseUrl
        {
            get { return "https://restapi.e-conomic.com/"; }
        }

        public static Dictionary<string, string> headerTokens
        {
            get
            {
                return new Dictionary<string, string>()
               {
                    { "X-AppSecretToken", "lT13QiMvnYdDOjIMNx5hwbCTpUtKbwUyk0J9K3BjEKo1"},
                    {"X-AgreementGrantToken" , "oIHRXORapYyj0P4PlMMIEGPJAY1B9t7lBKqbTZ5BNyQ1"}
               };
            }
        }
    }
}
