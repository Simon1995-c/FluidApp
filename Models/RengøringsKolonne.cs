using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    class RengøringsKolonne
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
    }
}
