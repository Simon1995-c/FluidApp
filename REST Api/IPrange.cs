namespace REST_Api
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("IPrange")]
    public partial class IPrange
    {
        
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string IP { get; set; }

        [Key]
        public int ID { get; set; }

    }
}
