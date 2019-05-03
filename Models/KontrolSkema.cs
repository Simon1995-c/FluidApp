using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Models
{
    public class KontrolSkema
    {
        public int ID { get; set; }
        public int FK_Kolonne { get; set; }
        public DateTime klokkeslæt { get; set; }
        public double Ludkoncetration { get; set; }
        public string Fustage { get; set; }
        public int Kvittering { get; set; }
        public double mS { get; set; }
        public bool LudKontrol { get; set; }
        public string Signatur { get; set; }
        public bool mSKontrol { get; set; }
        public double Vægt { get; set; }


        public const string URI = "http://localhost:52416/api/KontrolSkemas/";


        public List<KontrolSkema> GetAll()
        {
            List<KontrolSkema> returnList = new List<KontrolSkema>();

            using (HttpClient client = new HttpClient())
            {
                Task<string> resTask = client.GetStringAsync(URI);
                string jsonStr = resTask.Result;

                returnList = JsonConvert.DeserializeObject<List<KontrolSkema>>(jsonStr);
            }

            return returnList;
        }

        public KontrolSkema GetOne(int id)
        {
            KontrolSkema returnOne = new KontrolSkema();

            using (HttpClient client = new HttpClient())
            {
                Task<string> resTask = client.GetStringAsync(URI + id);
                string jsonStr = resTask.Result;

                returnOne = JsonConvert.DeserializeObject<KontrolSkema>(jsonStr);
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

        public void Post(KontrolSkema obj)
        {
            using (HttpClient client = new HttpClient())
            {
                string jsonStr = JsonConvert.SerializeObject(obj);
                StringContent content = new StringContent(jsonStr, Encoding.UTF8, "application/json");

                Task<HttpResponseMessage> postAsync = client.PostAsync(URI, content);

                HttpResponseMessage resp = postAsync.Result;
                if (resp.IsSuccessStatusCode)
                {
                    string jsonResStr = resp.Content.ReadAsStringAsync().Result;
                }
            }
        }

        public void Put(int id, KontrolSkema obj)
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