namespace REST_Api
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Kolonner")]
    public partial class Kolonner
    {
        public int ID { get; set; }
    }
}
