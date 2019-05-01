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
        public int ID { get; set; }
        public string IP { get; set; }
        

        public const string URI = "http://localhost:52416/api/IPranges/";

        public List<IPrange> GetAll()
        {
            List<IPrange> returnList = new List<IPrange>();

            using (HttpClient client = new HttpClient())
            {
                Task<string> resTask = client.GetStringAsync(URI);
                string jsonStr = resTask.Result;

                returnList = JsonConvert.DeserializeObject<List<IPrange>>(jsonStr);
            }

            return returnList;
        }

        public IPrange GetOne(int id)
        {
            IPrange returnOne = new IPrange();

            using (HttpClient client = new HttpClient())
            {
                Task<string> resTask = client.GetStringAsync(URI + id);
                string jsonStr = resTask.Result;

                returnOne = JsonConvert.DeserializeObject<IPrange>(jsonStr);
            }

            return returnOne;
        }

        public void Delete(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                Task<HttpResponseMessage> deleteAsync = client.DeleteAsync(URI + "/" + id);

                HttpResponseMessage resp = deleteAsync.Result;
                if (resp.IsSuccessStatusCode)
                {
                    String jsonStr = resp.Content.ReadAsStringAsync().Result;
                }
            }
        }

        public void Post(IPrange obj)
        {
            using (HttpClient client = new HttpClient())
            {
                string jsonStr = JsonConvert.SerializeObject(obj);
                StringContent content = new StringContent(jsonStr, Encoding.ASCII, "application/json");

                Task<HttpResponseMessage> postAsync = client.PostAsync(URI, content);

                HttpResponseMessage resp = postAsync.Result;
                if (resp.IsSuccessStatusCode)
                {
                    string jsonResStr = resp.Content.ReadAsStringAsync().Result;
                }
            }
        }

        public void Put(int id, IPrange obj)
        {
            using (HttpClient client = new HttpClient())
            {
                string jsonStr = JsonConvert.SerializeObject(obj);
                StringContent content = new StringContent(jsonStr, Encoding.UTF8, "application/json");

                Task<HttpResponseMessage> putAsync = client.PutAsync(URI + "/" + obj, content);

                HttpResponseMessage resp = putAsync.Result;
                if (resp.IsSuccessStatusCode)
                {
                    string jsonResStr = resp.Content.ReadAsStringAsync().Result;
                }
            }
        }
    }
}
