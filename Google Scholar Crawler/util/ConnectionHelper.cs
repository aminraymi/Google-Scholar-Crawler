using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Google_Scholar_Crawler.util
{
    static class ConnectionHelper
    {
        public static bool CheckConnectivity()
        {
            try
            {
                Ping connectionPing = new Ping();

                // define host to ping
                String host = "google.com";

                // set the ping timeout
                int timeout = 2000;

                byte[] buffer = new byte[32];
                
                PingReply reply = connectionPing.Send(host, timeout, buffer, new PingOptions());
                return (reply.Status == IPStatus.Success);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
