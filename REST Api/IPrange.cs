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
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IP { get; set; }
    }
}
