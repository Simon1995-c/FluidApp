using System;

namespace Models
{
    public class KontrolSkema
    {
        public int ID { get; set; }
        public int FK_Kolonne { get; set; }
        public DateTime klokkeslæt { get; set; }
        public double Ludkoncetration { get; set; }
        public DateTime VægtKontrol { get; set; }
        public bool Fustage { get; set; }
        public int Kvittering { get; set; }
        public double mS { get; set; }
        public bool LudKontrol { get; set; }
        public string Signatur { get; set; }
        public bool mSKontrol { get; set; }
        public double Vægt { get; set; }
    }
}