using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace REST_Api.Models
{
    [Table("Administrator")]
    public partial class Administrator
    {
        [Key]
        public int ID { get; set; }

        [StringLength(100)]
        public string Brugernavn { get; set; }

        [StringLength(100)]
        public string Kodeord { get; set; }

        public int? Rolle { get; set; }
    }
}
