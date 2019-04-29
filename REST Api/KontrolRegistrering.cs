namespace REST_Api
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KontrolRegistrering")]
    public partial class KontrolRegistrering
    {
        public int ID { get; set; }

        public int? FK_Kolonne { get; set; }

        public DateTime? Klokkeslæt { get; set; }

        public DateTime? Holdbarhedsdato { get; set; }

        public DateTime? Produktionsdato { get; set; }

        public int? FærdigvareNr { get; set; }

        [StringLength(250)]
        public string Kommentar { get; set; }

        public bool? Spritkontrol { get; set; }

        public int? HætteNr { get; set; }

        public int? EtiketNr { get; set; }

        [StringLength(100)]
        public string Fustage { get; set; }

        [StringLength(20)]
        public string Signatur { get; set; }
    }
}
