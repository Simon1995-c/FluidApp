namespace REST_Api
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KontrolSkema")]
    public partial class KontrolSkema
    {
        public int ID { get; set; }

        public int? FK_Kolonne { get; set; }

        public DateTime? Klokkeslæt { get; set; }

        public double? Ludkoncentration { get; set; }

        [StringLength(50)]
        public string Fustage { get; set; }

        public int? Kvittering { get; set; }

        public double? mS { get; set; }

        public bool? Ludkontrol { get; set; }

        [StringLength(20)]
        public string Signatur { get; set; }

        public bool? mSkontrol { get; set; }

        public double? Vægt { get; set; }
    }
}
