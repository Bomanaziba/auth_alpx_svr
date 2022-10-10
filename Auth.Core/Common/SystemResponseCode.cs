
using System.Collections.Generic;

namespace Auth.Core.Common
{
    
    public static class SystemResponseCode
    {

        public static List<string> SuccessResponseCode => new List<string>{ "00", "01" };

        public static List<string> PendingResponseCode => new List<string> { "02" };

        public static List<string> UserAlreadyExistResponseCode => new List<string>{ "04" };

          public static List<string> NoClientPassedResponseCode => new List<string>{ "05" };


        public static List<string> UnauthorizedResponseCode => new List<string>{ "07", "10" };

        public static List<string> FailedResponseCode => new List<string> { "03", "06" };

        public static string ResponseCodeDescription(string responseCode, string clientId="")
        {
            //TODO: a more detail response code description for terminal code

            if(SuccessResponseCode.Contains(responseCode)) return "SUCCESSFUL";

            if(UserAlreadyExistResponseCode.Contains(responseCode)) return "USER ALREADY EXIST";

            if(NoClientPassedResponseCode.Contains(responseCode)) return "NO CLIENT PASSED";

            if(UnauthorizedResponseCode.Contains(responseCode)) return "UNAUTHORIZED";

            if(PendingResponseCode.Contains(responseCode)) return "PENDING";

            if(FailedResponseCode.Contains(responseCode)) return "FAILED";    

            return "UNEXPECTED";          
        }
    }
}