namespace REST_Api
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Færdigvarekontrol
    {
        [Key]
        public int ProcessordreNr { get; set; }

        public int? FK_Kolonne { get; set; }

        public int? DåseNr { get; set; }

        [StringLength(20)]
        public string Initialer { get; set; }

        public int? LågNr { get; set; }

        public DateTime? DatoMærkning { get; set; }

        [StringLength(50)]
        public string LågFarve { get; set; }

        [StringLength(50)]
        public string RingFarve { get; set; }

        public int? Enheder { get; set; }

        [StringLength(300)]
        public string Parametre { get; set; }

        public int? Multipack { get; set; }

        public int? FolieNr { get; set; }

        public int? KartonNr { get; set; }

        public int? PalleNr { get; set; }
    }
}
