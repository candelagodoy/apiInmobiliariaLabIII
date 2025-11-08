
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apiInmobiliariaLabIII.Models
{
    [Table("inquilino")]
    public class Inquilino
    {
        [Key]
        public int idInquilino { get; set; }
        [MaxLength(50)]
        public string nombre { get; set; }
        [MaxLength(50)]
        public string apellido { get; set; }
        [MaxLength(50)]
        public string dni { get; set; }
        [MaxLength(50)]
        public string email { get; set; }
        [MaxLength(50)]
        public string telefono { get; set; }

    }
}

