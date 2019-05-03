using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Models
{
    public class Færdigvarekontrol
    {
        public int ProcessordreNr { get; set; }
        public int FK_Kolonne { get; set; }
        public int DåseNr { get; set; }
        public string Initialer { get; set; }
        public int LågNr { get; set; }
        public DateTime DatoMærkning { get; set; }
        public string LågFarve { get; set; }
        public string RingFarve { get; set; }
        public int Enheder { get; set; }
        public string Parametre { get; set; }
        public int Multipack { get; set; }
        public int FolieNr { get; set; }
        public int KartonNr { get; set; }
        public int PalleNr { get; set; }

        public const string URI = "https://restapi20190501124159.azurewebsites.net/api/Færdigvarekontrol";

        public List<Færdigvarekontrol> GetAll()
        {
            List<Færdigvarekontrol> returnList = new List<Færdigvarekontrol>();

            using (HttpClient client = new HttpClient())
            {
                Task<string> resTask = client.GetStringAsync(URI);
                string jsonStr = resTask.Result;

                returnList = JsonConvert.DeserializeObject<List<Færdigvarekontrol>>(jsonStr);
            }

            return returnList;
        }

        public Færdigvarekontrol GetOne(int id)
        {
            Færdigvarekontrol returnOne = new Færdigvarekontrol();

            using (HttpClient client = new HttpClient())
            {
                Task<string> resTask = client.GetStringAsync(URI + "/" + id);
                string jsonStr = resTask.Result;

                returnOne = JsonConvert.DeserializeObject<Færdigvarekontrol>(jsonStr);
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

        public void Post(Færdigvarekontrol obj)
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

        public void Put(int id, Færdigvarekontrol obj)
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
