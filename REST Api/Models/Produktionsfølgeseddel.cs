using System;
using System.ComponentModel.DataAnnotations;

namespace REST_Api.Models
{
    public partial class Produktionsfølgeseddel
    {
        public int ID { get; set; }

        public int? FK_Kolonne { get; set; }

        public DateTime? Slut { get; set; }

        public DateTime? Start { get; set; }

        public int? Bemanding { get; set; }

        public double? Timer { get; set; }

        [StringLength(20)]
        public string Signatur { get; set; }

        public int? Pauser { get; set; }
    }
}
