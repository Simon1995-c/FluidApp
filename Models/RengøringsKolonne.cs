using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Models
{
    public class RengøringsKolonne
    {
        public int ID { get; set; }
        public int KolonneNr { get; set; }
        public int UgeNr { get; set; }
        public string Kommentar { get; set; }
        public string Opgave { get; set; }
        public string Udstyr { get; set; }
        public string VejledningsNr { get; set; }
        public int Frekvens { get; set; }
        public DateTime SidstDato { get; set; }
        public DateTime IgenDato { get; set; }
        public string Udførsel { get; set; }
        public string Signatur { get; set; }



        public const string URI = "https://restapi20190501124159.azurewebsites.net/api/RengøringsKolonne";



        public List<RengøringsKolonne> GetAll()
        {
            List<RengøringsKolonne> returnList = new List<RengøringsKolonne>();

            using (HttpClient client = new HttpClient())
            {
                Task<string> resTask = client.GetStringAsync(URI);
                string jsonStr = resTask.Result;

                returnList = JsonConvert.DeserializeObject<List<RengøringsKolonne>>(jsonStr);
            }

            return returnList;
        }

        public RengøringsKolonne GetOne(int id)
        {
            RengøringsKolonne returnOne = new RengøringsKolonne();

            using (HttpClient client = new HttpClient())
            {
                Task<string> resTask = client.GetStringAsync(URI + "/" + id);
                string jsonStr = resTask.Result;

                returnOne = JsonConvert.DeserializeObject<RengøringsKolonne>(jsonStr);
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

        public void Post(RengøringsKolonne obj)
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

        public void Put(int id, RengøringsKolonne obj)
        {
            using (HttpClient client = new HttpClient())
            {
                string jsonStr = JsonConvert.SerializeObject(obj);
                StringContent content = new StringContent(jsonStr, Encoding.UTF8, "application/json");

                Task<HttpResponseMessage> putAsync = client.PutAsync(URI + "/" + id, content);

                HttpResponseMessage resp = putAsync.Result;
                if (resp.IsSuccessStatusCode)
                {
                    string jsonResStr = resp.Content.ReadAsStringAsync().Result;
                }
            }
        }
    }




}
