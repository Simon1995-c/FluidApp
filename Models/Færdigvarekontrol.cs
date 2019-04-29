using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{
    public class Færdigvarekontrol
    {
        public int ID { get; set; }
        public int FK_Kolonne { get; set; }
        public DateTime Klokkeslæt { get; set; }
        public DateTime Holdbarhedsdato { get; set; }
        public DateTime Produktionsdato { get; set; }
        public int FærdigvareNr { get; set; }
        public string Kommentar { get; set; }
        public bool Spritkontrol { get; set; }
        public int HætteNr { get; set; }
        public int EtiketNr { get; set; }
        public string Fustage { get; set; }
        public string Signatur { get; set; }
    }
}
