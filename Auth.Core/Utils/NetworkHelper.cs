


using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Auth.Core.Utils
{
    public class NetworkHelper
    {
        public static string GetMachineIpFormatted(bool excludeIPv6 = false)
        {
            var ips = Dns.GetHostAddresses(Dns.GetHostName());
            if(ips != null && ips.Any())
            {
                var sBuilder = new StringBuilder();
                sBuilder.Append("[ ");
                foreach(var ip in ips)
                {
                    if(ip.AddressFamily == AddressFamily.InterNetworkV6 && excludeIPv6)
                        continue;
                    sBuilder.AppendFormat("{0}, ", ip);
                }
                sBuilder.Append(" ]");
                return sBuilder.ToString();
            }
            return "";
        }
    }
}