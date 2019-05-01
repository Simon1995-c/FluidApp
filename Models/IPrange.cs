using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Models
{
    public class IPrange
    {
        public string IP { get; set; }

        public List<IPrange> GetAll()
        {
            List<IPrange> ipranges = new List<IPrange>();

            using (HttpClient client = new HttpClient())
            {
                Task<string> resTask = client.GetStringAsync("http://localhost:52416/api/IPranges");
                String jsonStr = resTask.Result;

                ipranges = JsonConvert.DeserializeObject<List<IPrange>>(jsonStr);
            }

            return ipranges;
        }
    }
}
