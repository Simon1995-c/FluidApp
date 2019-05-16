using System;
using System.ComponentModel.DataAnnotations;

namespace REST_Api.Models
{
    public partial class RengøringsKolonne
    {
        public int ID { get; set; }

        public int? KolonneNr { get; set; }

        public int? UgeNr { get; set; }

        [StringLength(250)]
        public string Kommentar { get; set; }

        [StringLength(300)]
        public string Opgave { get; set; }

        [StringLength(300)]
        public string Udstyr { get; set; }

        [StringLength(200)]
        public string VejledningsNr { get; set; }

        public int? Frekvens { get; set; }

        public DateTime? SidstDato { get; set; }

        public DateTime? IgenDato { get; set; }

        [StringLength(50)]
        public string Udførsel { get; set; }

        [StringLength(20)]
        public string Signatur { get; set; }
    }
}
