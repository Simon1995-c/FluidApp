using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace REST_Api.Models
{
    [Table("IPrange")]
    public partial class IPrange
    {
        
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string IP { get; set; }

        [Key]
        public int ID { get; set; }

    }
}
