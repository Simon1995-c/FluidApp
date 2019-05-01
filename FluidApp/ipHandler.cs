using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace FluidApp
{
    class ipHandler
    {
        private List<IPrange> allowedIps { get; }

        public ipHandler()
        {
            IPrange i = new IPrange();

            allowedIps = new List<IPrange>();

            foreach (var ip in i.GetAll())
            {
                allowedIps.Add(ip);
            }
        }

        private static async Task<string> getIp()
        {  
            return await new HttpClient().GetStringAsync("https://api.ipify.org/");
        }

        public async Task<bool> isAllowedIp()
        {
            //currnetIP venter til at den er færdig med sit kald så den ikke fortæstter uden at hente ipen.
            var currentIp = Task.Run(async () => { return await getIp(); }).Result;

            int validIp = 0;

            foreach (var ip in allowedIps)
            {
                if (ip.IP == currentIp)
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
