

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using apiInmobiliariaLabIIII.Models;

namespace apiInmobiliariaLabIII
{
    [Table("Pago")]
    public class Pago
    {
        [Key]
        public int idPago { get; set; }
        public DateTime fechaPago { get; set; }
        public int monto { get; set; }
        [MaxLength]
        public string detalle { get; set; }
        public Boolean estado { get; set; }
        public int idContrato { get; set; }
        [ForeignKey("idContrato")]
        public Contrato contrato { get; set; }
        
    }
}