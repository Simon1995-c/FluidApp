namespace REST_Api
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Administrator")]
    public partial class Administrator
    {
        [Key]
        [StringLength(100)]
        public string Brugernavn { get; set; }

        [StringLength(100)]
        public string Kodeord { get; set; }

        public int? Rolle { get; set; }
    }
}
