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
            allowedIps = new List<IPrange>();

            HttpClientHandler handler = new HttpClientHandler();

            using (HttpClient client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri("http://localhost:52416/");
                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    var response = client.GetAsync("api/IPranges").Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var ipranges = response.Content.ReadAsAsync<IEnumerable<IPrange>>().Result;
                        foreach (var ip in ipranges)
                        {
                            allowedIps.Add(ip);
                        }
                    }
                }
                catch (Exception)
                {
                    //Hvis der er noget der fejler, af en eller anden grund -> Send til error siden.
                    var frame = new Frame();
                    frame.Navigate(typeof(errorPageIPrange), null);
                    Window.Current.Content = frame;
                }
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
