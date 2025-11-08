
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using apiInmobiliariaLabIII.Models;

namespace apiInmobiliariaLabIIII.Models
{

    [Table("Contrato")]
    public class Contrato
    {
        [Key]
        public int idContrato { get; set; }
        public DateTime fechaInicio { get; set; }
        public DateTime fechaFinalizacion { get; set; }
        public decimal montoAlquiler { get; set; }
        public Boolean estado { get; set; }
        public int idInquilino { get; set; }
        [ForeignKey("idInquilino")]
        public Inquilino inquilino { get; set; }
        public int idInmueble { get; set; }
        [ForeignKey("idInmueble")]
        public Inmueble inmueble { get; set; }

    }
}
