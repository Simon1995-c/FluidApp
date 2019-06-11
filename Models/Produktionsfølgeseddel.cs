using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Models
{
    public class Produktionsfølgeseddel
    {
        public int ID { get; set; }
        public int FK_Kolonne { get; set; }
        public DateTime _slut;
        public DateTime _start;
        public int? Bemanding { get; set; }
        public double? Timer { get; set; }
        public string Signatur { get; set; }
        public int? Pauser { get; set; }
        public string FormattedStart { get; set; }
        public string FormattedSlut { get; set; }

        public DateTime Slut
        {
            get { return _slut; }
            set
            {
                _slut = value;
                FormattedSlut = _slut.TimeOfDay.ToString("hh\\:mm");
            }
        }

        public DateTime Start
        {
            get { return _start; }
            set
            {
                _start = value;
                FormattedStart = _start.TimeOfDay.ToString("hh\\:mm");
            }
        }


        public const string URI = "https://restapi20190611111326.azurewebsites.net/api/Produktionsfølgeseddel";


        public List<Produktionsfølgeseddel> GetAll()
        {
            List<Produktionsfølgeseddel> returnList = new List<Produktionsfølgeseddel>();

            using (HttpClient client = new HttpClient())
            {
                Task<string> resTask = client.GetStringAsync(URI);
                string jsonStr = resTask.Result;

                returnList = JsonConvert.DeserializeObject<List<Produktionsfølgeseddel>>(jsonStr);
            }

            return returnList;
        }

        public Produktionsfølgeseddel GetOne(int id)
        {
            Produktionsfølgeseddel returnOne = new Produktionsfølgeseddel();

            using (HttpClient client = new HttpClient())
            {
                Task<string> resTask = client.GetStringAsync(URI + "/" + id);
                string jsonStr = resTask.Result;

                returnOne = JsonConvert.DeserializeObject<Produktionsfølgeseddel>(jsonStr);
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

        public void Post(Produktionsfølgeseddel obj)
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

        public void Put(int id, Produktionsfølgeseddel obj)
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