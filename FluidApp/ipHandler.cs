using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FluidApp
{
    class ipHandler
    {
        private List<string> allowedIps { get; set; }

        public ipHandler()
        {
            allowedIps = new List<string>();
            allowedIps.Add("185.20.241.83");
        }

        private static string getIp()
        {
            var myIp = new HttpClient().GetStringAsync("https://api.ipify.org/");

            return myIp.Result;
        }

        public bool isAllowedIp()
        {
            string currentIp = getIp();
            int validIp = 0;

            foreach (var ip in allowedIps)
            {
                if (ip == currentIp)
                {
                    validIp++;
                }
            }

            if (validIp > 0)
            {
                return true;
            }

            return false;
        }
    }
}
